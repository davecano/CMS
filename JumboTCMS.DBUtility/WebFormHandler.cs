/*
 * 程序名称: JumboTCMS(将博内容管理系统通用版)
 * 
 * 程序版本: 7.x
 * 
 * 程序作者: 子木将博 (QQ：791104444@qq.com，仅限商业合作)
 * 
 * 版权申明: http://www.jumbotcms.net/about/copyright.html
 * 
 * 技术答疑: http://forum.jumbotcms.net/
 * 
 */

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web;
namespace JumboTCMS.DBUtility
{
    ///E:/swf/ <summary>
    ///E:/swf/ 处理表单中的数据，提供数据库添加、修改的通用处理过程。
    ///E:/swf/ 如果提交的数据被委托验证器认为无效则不作任何动作，否则操作完后引发操作完成事件。
    ///E:/swf/ </summary>
    public class WebFormHandler
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 将需要绑定的控件以object的形式存储在此数组中。
        ///E:/swf/ </summary>
        protected System.Collections.ArrayList alBinderItems = new System.Collections.ArrayList(8);
        ///E:/swf/ <summary>
        ///E:/swf/ 数据库连接，提供数据访问层的操作。
        ///E:/swf/ </summary>
        protected JumboTCMS.DBUtility.DbOperHandler doh;

        ///E:/swf/ <summary>
        ///E:/swf/ 用于存放从数据库中取出的数据记录。
        ///E:/swf/ </summary>
        protected DataTable myDt;
        //指示处理的提交数据是否通过验证

        ///E:/swf/ <summary>
        ///E:/swf/ 表示当前的操作类型，添加或修改，默认为添加。
        ///E:/swf/ </summary>
        private OperationType _mode = OperationType.Add;
        ///E:/swf/ <summary>
        ///E:/swf/ 指定当前的操作类型，可以指定为添加或修改。
        ///E:/swf/ </summary>
        public OperationType Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                if (value == OperationType.Add)
                {
                    btnSubmit.Text = AddText;
                }
                else if (value == OperationType.Copy)
                {
                    btnSubmit.Text = CopyText;
                }
                else if (value == OperationType.Modify)
                {
                    btnSubmit.Text = ModifyText;
                }
                else
                {
                    btnSubmit.Text = UnknownText;
                }
            }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 提交按钮在添加状态下显示的文本。
        ///E:/swf/ </summary>
        public string AddText = "确认";
        ///E:/swf/ <summary>
        ///E:/swf/ 提交按钮在修改状态下显示的文本。
        ///E:/swf/ </summary>
        public string ModifyText = "确认";

        ///E:/swf/ 提交按钮在修改状态下显示的文本。
        ///E:/swf/ </summary>
        public string CopyText = "复制";

        ///E:/swf/ <summary>
        ///E:/swf/ 提交按钮在未知状态下显示的文本。
        ///E:/swf/ </summary>
        public string UnknownText = "未知";

        ///E:/swf/ <summary>
        ///E:/swf/ 从数据库中取数据时的条件表达式。
        ///E:/swf/ </summary>
        private string _conditionExpress = string.Empty;
        ///E:/swf/ <summary>
        ///E:/swf/ 存储取得数据的表名称。
        ///E:/swf/ </summary>
        private string _tableName = string.Empty;

        ///E:/swf/ <summary>
        ///E:/swf/ 用于存储表单中的提交按钮的引用。
        ///E:/swf/ </summary>
        protected System.Web.UI.WebControls.Button btnSubmit = null;

        ///E:/swf/ <summary>
        ///E:/swf/ 指定取得数据的表名称。
        ///E:/swf/ </summary>
        public string TableName
        {
            set { _tableName = value; }
            get { return _tableName; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 指定从数据库中取数据时的条件表达式。
        ///E:/swf/ </summary>
        public string ConditionExpress
        {
            set { _conditionExpress = value; }
            get { return _conditionExpress; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 构造函数。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_doh">数据库操作对象。</param>
        ///E:/swf/ <param name="_table">使用的数据表的名称。</param>
        ///E:/swf/ <param name="_btn">表单中的提交按钮。</param>
        public WebFormHandler(JumboTCMS.DBUtility.DbOperHandler _doh, string _table, Button _btn)
        {
            TableName = _table;
            doh = _doh;
            btnSubmit = _btn;
            this.CheckArgs();
            if (!btnSubmit.Page.IsPostBack && this.Mode == OperationType.Add) this.BindWhenAdd();
            if (!btnSubmit.Page.IsPostBack && this.Mode == OperationType.Copy) this.BindWhenCopy();
            if (!btnSubmit.Page.IsPostBack && this.Mode == OperationType.Modify) this.BindWhenModify();
            btnSubmit.Click += new EventHandler(ProcessTheForm);
            btnSubmit.Page.PreRender += new EventHandler(this.Page_PreRender);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 增加数据时候预先将值填充到表单中。
        ///E:/swf/ </summary>
        public void BindWhenAdd()
        {
            this.OnBindBeforeAddOk(System.EventArgs.Empty);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 需要取出数据库中的记录填充到表单中。
        ///E:/swf/ </summary>
        public void BindWhenModify()
        {
            if (alBinderItems.Count == 0) return;
            BinderItem bi;
            int i = 0;

            StringBuilder sbSqlCmd = new StringBuilder("SELECT TOP 1 ");
            for (i = 0; i < alBinderItems.Count; i++)
            {
                bi = (BinderItem)alBinderItems[i];
                sbSqlCmd.Append(bi.field);
                sbSqlCmd.Append(",");
            }
            sbSqlCmd.Remove(sbSqlCmd.Length - 1, 1);//去掉多余的一个逗号
            sbSqlCmd.Append(" from ");
            sbSqlCmd.Append(TableName);
            sbSqlCmd.Append(" where 1=1 and ");
            sbSqlCmd.Append(this.ConditionExpress);

            doh.Reset();
            doh.SqlCmd = sbSqlCmd.ToString();
            myDt = doh.GetDataTable();
            //如果指定记录不存在则抛出异常
            if (myDt.Rows.Count == 0) throw new ArgumentException("Record does not exist.");

            DataRow dr = myDt.Rows[0];
            for (i = 0; i < alBinderItems.Count; i++)
            {
                bi = (BinderItem)alBinderItems[i];
                bi.SetValue(dr[bi.field].ToString());
            }
            this.OnBindBeforeModifyOk(System.EventArgs.Empty);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 需要取出数据库中的记录填充到表单中。
        ///E:/swf/ </summary>
        public void BindWhenCopy()
        {
            if (alBinderItems.Count == 0) return;
            BinderItem bi;
            int i = 0;

            StringBuilder sbSqlCmd = new StringBuilder("SELECT TOP 1 ");
            for (i = 0; i < alBinderItems.Count; i++)
            {
                bi = (BinderItem)alBinderItems[i];
                sbSqlCmd.Append(bi.field);
                sbSqlCmd.Append(",");
            }
            sbSqlCmd.Remove(sbSqlCmd.Length - 1, 1);//去掉多余的一个逗号
            sbSqlCmd.Append(" from ");
            sbSqlCmd.Append(TableName);
            sbSqlCmd.Append(" where 1=1 and ");
            sbSqlCmd.Append(this.ConditionExpress);

            doh.Reset();
            doh.SqlCmd = sbSqlCmd.ToString();
            myDt = doh.GetDataTable();
            //如果指定记录不存在则抛出异常
            if (myDt.Rows.Count == 0) throw new ArgumentException("Record does not exist.");

            DataRow dr = myDt.Rows[0];
            for (i = 0; i < alBinderItems.Count; i++)
            {
                bi = (BinderItem)alBinderItems[i];
                bi.SetValue(dr[bi.field].ToString());
            }
            this.OnBindBeforeCopyOk(System.EventArgs.Empty);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 当操作类型为添加时将输入添加到数据库中
        ///E:/swf/ </summary>
        protected void Add()
        {
            if (!DataValid()) return;
            if (alBinderItems.Count == 0) return;
            BinderItem bi;
            int i = 0;
            doh.Reset();

            for (i = 0; i < alBinderItems.Count; i++)
            {
                bi = (BinderItem)alBinderItems[i];
                //如果是单选按钮且没有选中则跳过这个字段
                if (bi.o is MyRadioButton)
                {
                    MyRadioButton mrb = (MyRadioButton)bi.o;
                    if (mrb.rb.Checked == false) continue;
                }

                doh.AddFieldItem(bi.field, bi.GetValue());
            }

            int id = doh.Insert(this.TableName);
            this.OnAddOk(new JumboTCMS.DBUtility.DbOperEventArgs(id));
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 当操作类型为修改时将输入更新到数据库中
        ///E:/swf/ </summary>
        protected void Update()
        {
            if (!DataValid()) return;
            if (alBinderItems.Count == 0) return;
            BinderItem bi;
            int i = 0;
            doh.Reset();
            for (i = 0; i < alBinderItems.Count; i++)
            {
                bi = (BinderItem)alBinderItems[i];
                //如果是单选按钮且没有选中则跳过这个字段
                if (bi.o is MyRadioButton)
                {
                    MyRadioButton mrb = (MyRadioButton)bi.o;
                    if (mrb.rb.Checked == false) continue;
                }

                doh.AddFieldItem(bi.field, bi.GetValue());
            }

            doh.ConditionExpress = this.ConditionExpress;
            doh.Update(this.TableName);
            this.OnModifyOk(System.EventArgs.Empty);
        }

        protected void Copy()
        {
            if (!DataValid()) return;
            if (alBinderItems.Count == 0) return;
            BinderItem bi;
            int i = 0;
            doh.Reset();
            for (i = 0; i < alBinderItems.Count; i++)
            {
                bi = (BinderItem)alBinderItems[i];
                //如果是单选按钮且没有选中则跳过这个字段
                if (bi.o is MyRadioButton)
                {
                    MyRadioButton mrb = (MyRadioButton)bi.o;
                    if (mrb.rb.Checked == false) continue;
                }

                doh.AddFieldItem(bi.field, bi.GetValue());
            }

            int id = doh.Insert(this.TableName);
            this.OnCopyOk(new JumboTCMS.DBUtility.DbOperEventArgs(id));
        }


        ///E:/swf/ <summary>
        ///E:/swf/ 建立文本框控件到数据库字段的绑定
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="tb">需要绑定的文本框对象。</param>
        ///E:/swf/ <param name="field">数据库中对应字段的名称。</param>
        ///E:/swf/ <param name="isStringType">是否为字符串性质。</param>
        public void AddBind(TextBox tb, string field, bool isStringType)
        {
            alBinderItems.Add(new BinderItem(tb, field, isStringType));
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 建立下拉框控件到数据库字段的绑定
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="ddl">需要绑定的下拉框对象。</param>
        ///E:/swf/ <param name="field">数据库中对应字段的名称。</param>
        ///E:/swf/ <param name="isStringType">是否为字符串性质。</param>
        public void AddBind(DropDownList ddl, string field, bool isStringType)
        {
            alBinderItems.Add(new BinderItem(ddl, field, isStringType));
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 建立复选框控件到数据库字段的绑定
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="cb">需要绑定的复选框对象。</param>
        ///E:/swf/ <param name="_value">复选框被选中时对应字段应该填写的值。</param>
        ///E:/swf/ <param name="field">数据库中对应字段的名称。</param>
        ///E:/swf/ <param name="isStringType">是否为字符串性质。</param>
        public void AddBind(CheckBox cb, string _value, string field, bool isStringType)
        {
            alBinderItems.Add(new BinderItem(new MyCheckBox(cb, _value), field, isStringType));
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 建立单选框控件到数据库字段的绑定
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="rb">需要绑定的单选框对象。</param>
        ///E:/swf/ <param name="_value">单选框被选中时对应字段应该填写的值。</param>
        ///E:/swf/ <param name="field">数据库中对应字段的名称。</param>
        ///E:/swf/ <param name="isStringType">是否为字符串性质。</param>
        public void AddBind(RadioButton rb, string _value, string field, bool isStringType)
        {
            alBinderItems.Add(new BinderItem(new MyRadioButton(rb, _value), field, isStringType));
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 建立Label控件到数据库字段的绑定
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="lbl">需要绑定的Label对象。</param>
        ///E:/swf/ <param name="field">数据库中对应字段的名称。</param>
        ///E:/swf/ <param name="isStringType">是否为字符串性质。</param>
        public void AddBind(Label lbl, string field, bool isStringType)
        {
            alBinderItems.Add(new BinderItem(lbl, field, isStringType));
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 建立Literal控件到数据库字段的绑定
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="ltl">需要绑定的Literal对象。</param>
        ///E:/swf/ <param name="field">数据库中对应字段的名称。</param>
        ///E:/swf/ <param name="isStringType">是否为字符串性质。</param>
        public void AddBind(Literal ltl, string field, bool isStringType)
        {
            alBinderItems.Add(new BinderItem(ltl, field, isStringType));
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 建立字符串变量到数据库字段的绑定。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="s">需要绑定的字符串引用。</param>
        ///E:/swf/ <param name="field">数据库中对应字段的名称。</param>
        ///E:/swf/ <param name="isStringType">是否为字符串性质。</param>
        public void AddBind(ref string s, string field, bool isStringType)
        {

            alBinderItems.Add(new BinderItem(ref s, field, isStringType));
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 建立一个委托对象到数据库字段的绑定。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="action">委托的名称。</param>
        ///E:/swf/ <param name="field">数据库中对应字段的名称。</param>
        ///E:/swf/ <param name="isStringType">是否为字符串性质。</param>
        private void AddBind(Action action, string field, bool isStringType)
        {

            alBinderItems.Add(new BinderItem(action, field, isStringType));
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 建立一个string类型属性到数据库字段的绑定。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_o">属性所在的对象。</param>
        ///E:/swf/ <param name="_propertyName">属性的名称。</param>
        ///E:/swf/ <param name="field">数据库中对应字段的名称。</param>
        ///E:/swf/ <param name="isStringType">是否为字符串性质。</param>
        public void AddBind(object _o, string _propertyName, string field, bool isStringType)
        {
            alBinderItems.Add(new BinderItem(new MyProperty(_o, _propertyName), field, isStringType));
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 检查是否输入了必须的参数，如表名，按钮对象。
        ///E:/swf/ </summary>
        public void CheckArgs()
        {
            if (TableName == string.Empty)
            {
                throw new ArgumentException("None table name is specified!");
            }
            if (btnSubmit == null)
            {
                throw new ArgumentException("None submit button is specified!");
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 捕获按钮点击事件，处理表单，添加或修改
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="sender">触发对象。</param>
        ///E:/swf/ <param name="e">传递的事件参数。</param>
        private void ProcessTheForm(object sender, System.EventArgs e)
        {
            if (Mode == OperationType.Add)
            {
                this.Add();
            }
            else if (Mode == OperationType.Copy)
            {
                this.Copy();
            }
            else if (Mode == OperationType.Modify)
            {
                this.Update();
            }
            else
            {
                throw new ArgumentException("Unkown operation type.");
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 表单验证器，现仅支持一个验证器，委托链长度不得多于一个。
        ///E:/swf/ </summary>
        public delegate bool Validator();
        ///E:/swf/ <summary>
        ///E:/swf/ 存储委托对象。
        ///E:/swf/ </summary>
        public Validator validator = null;

        ///E:/swf/ <summary>
        ///E:/swf/ 使用委托验证器验证传递过来的数据是否合法
        ///E:/swf/ </summary>
        ///E:/swf/ <returns>提交的数据是否符合用户定义的逻辑。</returns>
        private bool DataValid()
        {
            bool validOk = true;
            if (validator != null)
            {
                validOk = validator();
            }
            return validOk;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 添加数据完成事件，当数据被添加到数据库之后触发。
        ///E:/swf/ </summary>
        public event System.EventHandler AddOk;
        ///E:/swf/ <summary>
        ///E:/swf/添加数据完成事件，当数据被添加到数据库之后触发。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="e">添加完成的事件参数。</param>
        protected virtual void OnAddOk(System.EventArgs e)
        {
            if (AddOk != null)
            {
                AddOk(this, e);
            }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 修改数据完成事件，当数据被更新到数据库之后触发。
        ///E:/swf/ </summary>
        public event System.EventHandler ModifyOk;
        ///E:/swf/ <summary>
        ///E:/swf/ 修改完成事件，当数据被更新到数据库之后触发。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="e">修改完成的事件参数。</param>
        protected virtual void OnModifyOk(System.EventArgs e)
        {
            if (ModifyOk != null)
            {
                ModifyOk(this, e);
            }
        }


        ///E:/swf/ <summary>
        ///E:/swf/ 复制数据完成事件，当数据被更新到数据库之后触发。
        ///E:/swf/ </summary>
        public event System.EventHandler CopyOk;
        ///E:/swf/ <summary>
        ///E:/swf/ 复制完成事件，当数据被更新到数据库之后触发。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="e">复制完成的事件参数。</param>
        protected virtual void OnCopyOk(System.EventArgs e)
        {
            if (CopyOk != null)
            {
                CopyOk(this, e);
            }
        }


        ///E:/swf/ <summary>
        ///E:/swf/ 此事件在页面显示前触发。
        ///E:/swf/ </summary>
        public event System.EventHandler BindBeforeAddOk;
        ///E:/swf/ <summary>
        ///E:/swf/ 此事件在页面显示前触发。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="e">事件参数。</param>
        protected virtual void OnBindBeforeAddOk(System.EventArgs e)
        {
            if (BindBeforeAddOk != null)
            {
                BindBeforeAddOk(this, e);
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 当修改时，数据库中的字段值会被填充到相应绑定的控件中，此事件在填充完成后，显示到页面之前触发。
        ///E:/swf/ </summary>
        public event System.EventHandler BindBeforeModifyOk;
        ///E:/swf/ <summary>
        ///E:/swf/ 当修改时，数据库中的字段值会被填充到相应绑定的控件中，此事件在填充完成后，显示到页面之前触发。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="e">事件参数。</param>
        protected virtual void OnBindBeforeModifyOk(System.EventArgs e)
        {
            if (BindBeforeModifyOk != null)
            {
                BindBeforeModifyOk(this, e);
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 当复制时，数据库中的字段值会被填充到相应绑定的控件中，此事件在填充完成后，显示到页面之前触发。
        ///E:/swf/ </summary>
        public event System.EventHandler BindBeforeCopyOk;
        ///E:/swf/ <summary>
        ///E:/swf/ 当复制时，数据库中的字段值会被填充到相应绑定的控件中，此事件在填充完成后，显示到页面之前触发。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="e">事件参数。</param>
        protected virtual void OnBindBeforeCopyOk(System.EventArgs e)
        {
            if (BindBeforeCopyOk != null)
            {
                BindBeforeCopyOk(this, e);
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 在页面预生成时，提交到浏览器之前，检查是否需要将数据库中的数据填充到表单中。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="sender">传递的对象。</param>
        ///E:/swf/ <param name="e">传递的事件参数。</param>
        private void Page_PreRender(object sender, EventArgs e)
        {
            if (this.Mode == OperationType.Add && !btnSubmit.Page.IsPostBack)
            {
                BindWhenAdd();
            }
            if (this.Mode == OperationType.Copy && !btnSubmit.Page.IsPostBack)
            {
                BindWhenCopy();
            }
            if (this.Mode == OperationType.Modify && !btnSubmit.Page.IsPostBack)
            {
                BindWhenModify();
            }
        }
    }
    ///E:/swf/ <summary>
    ///E:/swf/ 定义的委托原型。
    ///E:/swf/ </summary>
    public delegate string Action(string s);
    #region 自定义用来支持表单处理的类
    ///E:/swf/ <summary>
    ///E:/swf/ 每个和数据库字段绑定的对象以BinderItem为容器存储在数组中。
    ///E:/swf/ </summary>
    public class BinderItem
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 每个绑定控件都是以object的形式被存储的。
        ///E:/swf/ </summary>
        public object o;
        ///E:/swf/ <summary>
        ///E:/swf/ 绑定到数据库的字段名称。
        ///E:/swf/ </summary>
        public string field;
        ///E:/swf/ <summary>
        ///E:/swf/ 是否是字符串类型。
        ///E:/swf/ </summary>
        public bool isStringType;
        ///E:/swf/ <summary>
        ///E:/swf/ 构造函数。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_o">需要绑定的控件对象。</param>
        ///E:/swf/ <param name="_field">绑定到的数据表字段名称。</param>
        ///E:/swf/ <param name="_isStringType">是否是字符串类型。</param>
        public BinderItem(object _o, string _field, bool _isStringType)
        {
            this.o = _o;
            //this.field = ("[" + _field + "]").Replace("[[","[").Replace("]]","]");
            this.field = _field;
            this.isStringType = _isStringType;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 构造函数。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_o">需要绑定的string变量引用。</param>
        ///E:/swf/ <param name="_field">绑定到的数据表字段名称。</param>
        ///E:/swf/ <param name="_isStringType">是否是字符串类型。</param>
        public BinderItem(ref string _o, string _field, bool _isStringType)
        {
            this.o = _o;
            //this.field = ("[" + _field + "]").Replace("[[","[").Replace("]]","]");
            this.field = _field;
            this.isStringType = _isStringType;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 根据控件类型获得控件的值。
        ///E:/swf/ </summary>
        ///E:/swf/ <returns>控件的值。</returns>
        public string GetValue()
        {
            //属性类型
            if (o is MyProperty)
            {
                MyProperty mp = (MyProperty)o;
                System.Type type = mp.po.GetType();
                System.Reflection.PropertyInfo pi = type.GetProperty(mp.propertyName);
                return (string)pi.GetValue(mp.po, null);
                //return type.InvokeMember(mp.propertyName,System.Reflection.BindingFlags.GetProperty,null,mp.po,);
                //return mp.propertyName;
            }

            //字符串类型
            if (o is String)
            {
                string s = (string)o;
                return s;

            }
            //方法委托
            if (o is Action)
            {
                Action action = (Action)o;
                return action("_g_e_t_");
            }
            //下拉框
            if (o is DropDownList)
            {
                DropDownList ddl = (DropDownList)o;
                return ddl.SelectedValue;

            }
            //复选框
            if (o is MyCheckBox)
            {
                MyCheckBox mcb = (MyCheckBox)o;
                if (mcb.cb.Checked) return mcb.Value; else return "0";

            }
            //单选按钮
            if (o is MyRadioButton)
            {
                MyRadioButton mrb = (MyRadioButton)o;
                if (mrb.rb.Checked) return mrb.Value;
            }
            //文本框
            if (o is TextBox)
            {
                TextBox tb = (TextBox)o;
                return tb.Text;
            }
            //Label
            if (o is Label)
            {
                Label lbl = (Label)o;
                return lbl.Text;
            }
            //Literal
            if (o is Literal)
            {
                Literal ltl = (Literal)o;
                return ltl.Text;
            }
            return string.Empty;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 根据控件类型设定控件的值
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_value">要设定的值。</param>
        public void SetValue(string _value)
        {
            //属性类型
            if (o is MyProperty)
            {
                MyProperty mp = (MyProperty)o;
                System.Type type = mp.po.GetType();
                System.Reflection.PropertyInfo pi = type.GetProperty(mp.propertyName);
                pi.SetValue(mp.po, _value, null);
                //return type.InvokeMember(mp.propertyName,System.Reflection.BindingFlags.GetProperty,null,mp.po,);
                //return mp.propertyName;
            }

            //字符串类型
            if (o is String)
            {
                //this.SetString((string)o,_value);
                //return;
                string s = (string)o;
                s = _value;
                return;
            }
            //委托
            if (o is Action)
            {
                Action action = (Action)o;
                action(_value);
                return;
            }
            //下拉框
            if (o is DropDownList)
            {
                DropDownList ddl = (DropDownList)o;
                ListItem li = ddl.Items.FindByValue(_value);
                if (li != null)
                {
                    ddl.ClearSelection();
                    li.Selected = true;
                }
                return;
            }
            //复选框
            if (o is MyCheckBox)
            {
                MyCheckBox mcb = (MyCheckBox)o;
                if (mcb.Value == _value) mcb.cb.Checked = true;
                return;
            }
            //单选按钮
            if (o is MyRadioButton)
            {
                MyRadioButton mrb = (MyRadioButton)o;
                if (mrb.Value == _value) mrb.rb.Checked = true;
                return;
            }
            //文本框
            if (o is TextBox)
            {
                TextBox tb = (TextBox)o;
                tb.Text = _value;
                return;
            }
            //Label
            if (o is Label)
            {
                Label lbl = (Label)o;
                lbl.Text = _value;
                return;
            }
            //Literal
            if (o is Literal)
            {
                Literal ltl = (Literal)o;
                ltl.Text = _value;
                return;
            }
        }

        private void SetString(string s, string _value)
        {
            s = _value;
        }

    }

    ///E:/swf/ <summary>
    ///E:/swf/ 扩展复选框，可以使CheckBox具有Value属性，选中时表现。
    ///E:/swf/ </summary>
    public class MyCheckBox
    {
        ///E:/swf/ <summary>
        ///E:/swf/ CheckBox对象。
        ///E:/swf/ </summary>
        public CheckBox cb;
        ///E:/swf/ <summary>
        ///E:/swf/ 选中时应该表现的值。
        ///E:/swf/ </summary>
        public string Value;
        ///E:/swf/ <summary>
        ///E:/swf/ 构造函数。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_cb">CheckBox对象。</param>
        ///E:/swf/ <param name="_value">选中时应该表现的值。</param>
        public MyCheckBox(CheckBox _cb, string _value)
        {
            cb = _cb;
            Value = _value;
        }
    }

    ///E:/swf/ <summary>
    ///E:/swf/ 扩展单选按钮，可以使RadioButton具有Value属性，选中时表现。
    ///E:/swf/ </summary>
    public class MyRadioButton
    {
        ///E:/swf/ <summary>
        ///E:/swf/ RadioButton对象。
        ///E:/swf/ </summary>
        public RadioButton rb;
        ///E:/swf/ <summary>
        ///E:/swf/ 选中时应该表现的值。
        ///E:/swf/ </summary>
        public string Value;
        ///E:/swf/ <summary>
        ///E:/swf/ 构造函数。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_rb">RadioButton对象。</param>
        ///E:/swf/ <param name="_value">选中时应该表现的值。</param>
        public MyRadioButton(RadioButton _rb, string _value)
        {
            rb = _rb;
            Value = _value;
        }
    }

    ///E:/swf/ <summary>
    ///E:/swf/ 扩展属性，存储一个对象引用和需要绑定的属性名称。
    ///E:/swf/ </summary>
    public class MyProperty
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 要绑定属性所在的对象。
        ///E:/swf/ </summary>
        public object po;
        ///E:/swf/ <summary>
        ///E:/swf/ 要绑定的属性名称。
        ///E:/swf/ </summary>
        public string propertyName;
        ///E:/swf/ <summary>
        ///E:/swf/ 构造函数。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_o">要绑定属性所在的对象。</param>
        ///E:/swf/ <param name="_propertyName">要绑定的属性名称。</param>
        public MyProperty(object _o, string _propertyName)
        {
            po = _o;
            propertyName = _propertyName;
        }
    }

    #endregion

}
