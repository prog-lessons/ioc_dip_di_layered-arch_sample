using System;
using System.Collections.Generic;

namespace ProgLessons.IoC_example.App.Services.UI
{
    public abstract class UserInterfaceManager : IUserInterfaceService
    {
        public Action RequestAppStop { get; protected set; }
        public bool SingleThreaded { get; protected set; }

        protected List<UIField> inputFields;
        protected Action inputPerformedFunc;

        public abstract void BreakLine();
        public abstract void OutputInfo(string text, InfoCategory infoCategory);

        public virtual void WaitUserInput(List<UIField> inputFields, Action inputPerformedFunc)
        {
            this.inputFields = inputFields;
            this.inputPerformedFunc = inputPerformedFunc;
        }

        public virtual UIField GetUIField(string fieldLabel)
        {
            return inputFields.Find(f => f.Label == fieldLabel);
        }
    }
}