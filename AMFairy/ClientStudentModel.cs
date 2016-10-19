namespace ExamSysWinform.WebService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, XmlType(Namespace="http://tempuri.org/"), DesignerCategory("code"), GeneratedCode("System.Xml", "4.0.30319.34230"), DebuggerStepThrough]
    public class ClientStudentModel : ClientModelSuper
    {
        private int classidField;
        private bool enAbleExerciseField;
        private bool enableLoginField;
        private bool isSameVersionField;
        private string messageInfo1Field;
        private string pwdField;
        private string teacherNameField;
        private string versionField;

        public int Classid
        {
            get
            {
                return this.classidField;
            }
            set
            {
                this.classidField = value;
            }
        }

        public bool EnAbleExercise
        {
            get
            {
                return this.enAbleExerciseField;
            }
            set
            {
                this.enAbleExerciseField = value;
            }
        }

        public bool EnableLogin
        {
            get
            {
                return this.enableLoginField;
            }
            set
            {
                this.enableLoginField = value;
            }
        }

        public bool IsSameVersion
        {
            get
            {
                return this.isSameVersionField;
            }
            set
            {
                this.isSameVersionField = value;
            }
        }

        public string MessageInfo1
        {
            get
            {
                return this.messageInfo1Field;
            }
            set
            {
                this.messageInfo1Field = value;
            }
        }

        public string Pwd
        {
            get
            {
                return this.pwdField;
            }
            set
            {
                this.pwdField = value;
            }
        }

        public string TeacherName
        {
            get
            {
                return this.teacherNameField;
            }
            set
            {
                this.teacherNameField = value;
            }
        }

        public string Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }
}

