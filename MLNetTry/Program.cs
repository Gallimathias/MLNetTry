using Microsoft.ML;
using Microsoft.ML.Data;
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

            var textLoader = context.Data.CreateTextLoader(
                columns: new[]
                {
                        new TextLoader.Column("Label", DataKind.UInt32, 0),
                        new TextLoader.Column("Text", DataKind.String, 1)
                },
                separatorChar: ';',
                hasHeader: true
            );

            var trainingDataView = textLoader.Load(new MultiFileSource(@".\TrainingData.txt"));

            //Create and Train model
            Console.WriteLine("...succesfull");
            Console.Write("Create Model");
            var pipeline = context
                                  .Transforms
                                  .Text
                                  .FeaturizeText(outputColumnName: "Features", inputColumnName: "Text")
                                  .Append(context.Transforms.Conversion.MapValueToKey(outputColumnName: "Label_Key", inputColumnName: "Label"))
                                  .Append(context.MulticlassClassification.Trainers.SdcaMaximumEntropy(labelColumnName: "Label_Key", featureColumnName: "Features"))
                                  .Append(context.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            Console.WriteLine("...succesfull");
            Console.Write("Train Model");
            var model = pipeline.Fit(trainingDataView);

            //Evaluate model
            Console.WriteLine("...succesfull");
            Console.Write("Evaluate Model");
            var testDataView = textLoader.Load(new MultiFileSource(@".\TestData.txt"));

            var predictions = model.Transform(testDataView);
            var metrics = context.MulticlassClassification.Evaluate(predictions, labelColumnName: "Label_Key", scoreColumnName: "Score");

            //Test model
            Console.WriteLine("...succesfull");
            Console.WriteLine("Test Model");
            var predictionFunct = context.Model.CreatePredictionEngine<ChatMessage, ChatIntentPrediction>(model);
            
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
                var values = new VBuffer<uint>();
                predictionFunct.OutputSchema["Score"].Annotations.GetValue("TrainingLabelValues", ref values);
                
                foreach (var labelScore in values.Items())
                {
                    resultList.Add(new IntentResult(labelScore.Value, resultPrediction.Score[labelScore.Key]));
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
