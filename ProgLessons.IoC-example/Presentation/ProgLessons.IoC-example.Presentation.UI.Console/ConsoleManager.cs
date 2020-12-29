using System;
using System.Collections.Generic;
using ProgLessons.IoC_example.App.Services.UI;
using static System.Console;

namespace ProgLessons.IoC_example.Presentation.UI.Console
{
    public class ConsoleManager : UserInterfaceManager
    {
        public override void OutputInfo(string text, InfoCategory infoCategory)
        {
            switch (infoCategory)
            {
                case InfoCategory.Title:
                    BreakLine();
                    ForegroundColor = ConsoleColor.Cyan;
                    BreakLine();
                    break;
                case InfoCategory.SubTitle:
                    ForegroundColor = ConsoleColor.Green;
                    break;

                case InfoCategory.Default:
                    ForegroundColor = ConsoleColor.Gray;
                    break;
            }
            Write(text);
        }

        public override void BreakLine() => WriteLine();

        public override void WaitUserInput(List<UIField> inputFields, Action inputPerformedFunc)
        {
            if (this.inputFields == null)
                base.WaitUserInput(inputFields, inputPerformedFunc);

            BreakLine();
            foreach (var f in inputFields)
            {
                OutputInfo(f.Label + ": ", InfoCategory.Default);
                if (f.HideChars)
                {
                    ForegroundColor = ConsoleColor.Black;
                    f.Value = ReadLine();
                    ResetColor();
                }
                else
                    f.Value = ReadLine();
            }

            inputPerformedFunc.Invoke();
            BreakLine();
        }

        public ConsoleManager() => this.SingleThreaded = true;
    }
}