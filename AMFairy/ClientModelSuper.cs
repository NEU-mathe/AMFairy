namespace ExamSysWinform.WebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, XmlType(Namespace="http://tempuri.org/"), DesignerCategory("code"), XmlInclude(typeof(ClientExamModel)), XmlInclude(typeof(ClientStudentModel)), GeneratedCode("System.Xml", "4.0.30319.34230"), DebuggerStepThrough]
    public class ClientModelSuper
    {
        private string classNameField;
        private string dataSourceField;
        private string keyField;
        private string studentNameField;
        private string studentNumberField;
        private string teacherIdField;

        public string ClassName
        {
            get
            {
                return this.classNameField;
            }
            set
            {
                this.classNameField = value;
            }
        }

        public string DataSource
        {
            get
            {
                return this.dataSourceField;
            }
            set
            {
                this.dataSourceField = value;
            }
        }

        public string Key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }

        public string StudentName
        {
            get
            {
                return this.studentNameField;
            }
            set
            {
                this.studentNameField = value;
            }
        }

        public string StudentNumber
        {
            get
            {
                return this.studentNumberField;
            }
            set
            {
                this.studentNumberField = value;
            }
        }

        public string TeacherId
        {
            get
            {
                return this.teacherIdField;
            }
            set
            {
                this.teacherIdField = value;
            }
        }
    }
}

