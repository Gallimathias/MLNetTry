using Microsoft.ML;
using Microsoft.ML.Core.Data;
using Microsoft.ML.Runtime.Data;
using Microsoft.ML.Runtime.Learners;
using Microsoft.ML.Transforms;
using Microsoft.ML.Transforms.Categorical;
using Microsoft.ML.Transforms.Normalizers;
using MLTetris.ML.Data;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Microsoft.ML.Transforms.Normalizers.NormalizingEstimator;

namespace MLTetris.ML
{
    public abstract class Ai
    {
        private TransformerChain<ITransformer> model;
        private PredictionFunction<GameData, ControlIntent> function;
        private MLContext context;
        private Logger logger;

        public Ai()
        {
            context = new MLContext();
            logger = LogManager.GetCurrentClassLogger();
        }

        public void Build(string fullName)
        {
            //Create DataReader for Training
            logger.Info("Create Environment");

            var reader = new TextLoader(context, new TextLoader.Arguments()
            {
                Separator = ",",
                HasHeader = true,
                Column = new[]
                {
                        new TextLoader.Column("Key", DataKind.R4, 0),
                        new TextLoader.Column("IsKeyUp", DataKind.Bool, 1),
                        new TextLoader.Column("FigureType", DataKind.Text, 2),
                        new TextLoader.Column("MaxX", DataKind.R4, 3),
                        new TextLoader.Column("MaxY", DataKind.R4, 4),
                        new TextLoader.Column("MinX", DataKind.R4, 5),
                        new TextLoader.Column("MinY", DataKind.R4, 6),
                    }
            });

            var trainingDataView = reader.Read(new MultiFileSource(fullName));

            //Create and Train model
            logger.Info("...succesfull");
            logger.Info("Create Model");
            var pipeline = context.Transforms.CopyColumns("Key", "Label")
                .Append(context.Transforms.Categorical.OneHotEncoding(inputColumn: "FigureType", outputColumn: "FigureTypeEncoded"))
                .Append(context.Transforms.Normalize(inputName: "MaxX", mode: NormalizerMode.MeanVariance))
                .Append(context.Transforms.Normalize(inputName: "MaxY", mode: NormalizerMode.MeanVariance))
                .Append(context.Transforms.Normalize(inputName: "MinX", mode: NormalizerMode.MeanVariance))
                .Append(context.Transforms.Normalize(inputName: "MinY", mode: NormalizerMode.MeanVariance))
                .Append(context.Transforms.Concatenate("Features", "FigureTypeEncoded", "MaxX", "MaxY", "MinX", "MinY"));

            var trainer = context.Regression.Trainers.StochasticDualCoordinateAscent(label: "Label", features: "Features");

            pipeline.Append(trainer);

            logger.Info("...succesfull");
            logger.Info("Train Model");
            var model = pipeline.Fit(trainingDataView);

            logger.Info("...succesfull");
            
            using (var fileStream = File.OpenWrite(@".\thea.brain"))
                context.Model.Save(model, fileStream);
        }

        public void Load(string fullName)
        {
            logger.Info("Load model");

            using (var fileStream = File.OpenRead(@".\thea.brain"))
                model = TransformerChain.LoadFrom(context, fileStream);

            function = model.MakePredictionFunction<GameData, ControlIntent>(context);
        }

        public ControlIntent Predict(GameData data)
            => function.Predict(data);
    }
}