using MLTetris.Figures;
using MLTetris.ML.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLTetris.ML
{
    public class Observer
    {
        private readonly List<ControlCommand> data;

        public Observer()
        {
            data = new List<ControlCommand>();
        }

        internal void AddKeyEvent(Keys keyData, bool keyup, BaseFigure currentFigure)
        {
            data.Add(new ControlCommand()
            {
                Key = keyData,
                IsKeyUp = keyup,
                FigureType = currentFigure.GetType(),
                MaxX = currentFigure.BrickPositions.Max(p => p.X),
                MaxY = currentFigure.BrickPositions.Max(p => p.Y),
                MinX = currentFigure.BrickPositions.Min(p => p.X),
                MinY = currentFigure.BrickPositions.Min(p => p.Y)
            });
        }

        internal void Write(string fullName)
        {
            var builder = new StringBuilder();
            builder.AppendLine(
                nameof(ControlCommand.Key) + ',' +
                nameof(ControlCommand.IsKeyUp) + ',' +
                nameof(ControlCommand.FigureType) + ',' +
                nameof(ControlCommand.MaxX) + ',' +
                nameof(ControlCommand.MaxY) + ',' +
                nameof(ControlCommand.MinX) + ',' +
                nameof(ControlCommand.MinY)
                );

            foreach (var line in data.Select(d => d.ToString()))
                builder.AppendLine(line);

            var fileInfo = new FileInfo(fullName);

            if (fileInfo.Exists)
                fileInfo.Delete();

            File.WriteAllText(fullName, builder.ToString());
        }

    }
}
