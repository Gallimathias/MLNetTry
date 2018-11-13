using Microsoft.ML;
using Microsoft.ML.Core.Data;
using Microsoft.ML.Runtime.Data;
using Microsoft.ML.Runtime.Learners;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MLNetTry
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var context = new MLContext();

            //Create DataReader for Training
            Console.Write("Create Environment");

            var reader = context.Data.TextReader(new TextLoader.Arguments()
            {
                Separator = ";",
                HasHeader = true,
                Column = new[]
                {
                        new TextLoader.Column("Label", DataKind.R4, 0),
                        new TextLoader.Column("Text", DataKind.Text, 1)
                    }
            });

            var trainingDataView = reader.Read(new MultiFileSource(@".\TrainingData.txt"));

            //Create and Train model
            Console.WriteLine("...succesfull");
            Console.Write("Create Model");
            var pipeline = context
                                  .Transforms
                                  .Text
                                  .FeaturizeText("Text", "Features")
                                  .Append(context.MulticlassClassification.Trainers.StochasticDualCoordinateAscent(label: "Label", features: "Features"));


            Console.WriteLine("...succesfull");
            Console.Write("Train Model");
            var model = pipeline.Fit(trainingDataView);

            //Evaluate model
            Console.WriteLine("...succesfull");
            Console.Write("Evaluate Model");
            var testDataView = reader.Read(new MultiFileSource(@".\TestData.txt"));

            var predictions = model.Transform(testDataView);
            var metrics = context.MulticlassClassification.Evaluate(predictions, "Label", "Score");

            //Test model
            Console.WriteLine("...succesfull");
            Console.WriteLine("Test Model");
            var predictionFunct = model.MakePredictionFunction<ChatMessage, ChatIntentPrediction>(context);

            var sample = new ChatMessage()
            {
                Text = "Hallo Welt"
            };

            var resultList = new List<IntentResult>();
            Console.WriteLine();
            Console.WriteLine($"Testresult of example {sample.Text} is: ");

            do
            {
                resultList.Clear();
                var resultPrediction = predictionFunct.Predict(sample);

                for (int i = 0; i < resultPrediction.Score.Length; i++)
                {
                    resultList.Add(new IntentResult(i, resultPrediction.Score[i]));
                }

                foreach (var item in resultList.OrderByDescending(i => i.Score))
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine();
                Console.Write("Message: ");
                sample = new ChatMessage()
                {
                    Text = Console.ReadLine()
                };

                Console.Clear();
                Console.WriteLine("Result is: ");

            } while (true);

        }
    }
}
