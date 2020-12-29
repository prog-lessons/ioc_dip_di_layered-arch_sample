using System;
using System.Collections.Generic;

public enum InfoCategory { Title, SubTitle, Default, Response, FixedResponse };

namespace ProgLessons.IoC_example.App.Services.UI
{
    public class UIField
    {
        public string Label { get; private set; }
        public string Value { get; set; }
        public bool HideChars { get; set; }

        public UIField(string label)
        {
            Label = label;
            Value = "";
        }
    }

    public interface IUserInterfaceService
    {
        Action RequestAppStop { get; }
        bool SingleThreaded { get; }

        void WaitUserInput(List<UIField> inputFields, Action InputPerformedFunc);
        void OutputInfo(string text, InfoCategory infoCategory);
        void BreakLine();
        UIField GetUIField(string fieldLabel);
    }
}