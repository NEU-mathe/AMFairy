namespace ExamSysWinform.WebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, XmlType(Namespace="http://tempuri.org/"), DebuggerStepThrough, DesignerCategory("code"), GeneratedCode("System.Xml", "4.0.30319.34230")]
    public class ClientExamModel : ClientModelSuper
    {
        private string id1Field;
        private string randAnwserField;
        private int scoreField;
        private string studentAnwserField;
        private int tableTypeField;
        private string templateNameField;

        public string Id1
        {
            get
            {
                return this.id1Field;
            }
            set
            {
                this.id1Field = value;
            }
        }

        public string RandAnwser
        {
            get
            {
                return this.randAnwserField;
            }
            set
            {
                this.randAnwserField = value;
            }
        }

        public int Score
        {
            get
            {
                return this.scoreField;
            }
            set
            {
                this.scoreField = value;
            }
        }

        public string StudentAnwser
        {
            get
            {
                return this.studentAnwserField;
            }
            set
            {
                this.studentAnwserField = value;
            }
        }

        public int TableType
        {
            get
            {
                return this.tableTypeField;
            }
            set
            {
                this.tableTypeField = value;
            }
        }

        public string TemplateName
        {
            get
            {
                return this.templateNameField;
            }
            set
            {
                this.templateNameField = value;
            }
        }
    }
}

