using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Cshandler.CompilationExecutionEngine;

namespace Cshandler.csfiddle.web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtMain.Text = "Test test = new Test(); \n" + "Print(test.SayHello()); \n"
                               + "Print(\"Special symbols are running here\"); \n" + "var i = 10; \n"
                               + "Print(i.ToString()); \n";

                txtSourceLib.Text = "using System; \n " + "public class Test \n " + "{ \n "
                                    + "  public string SayHello()  \n " + "  { \n " + "  return \"Hi, WWW.\"; \n "
                                    + "  } \n " + "} \n ";
            }
        }

        protected void btnCompileAndRun_Click(object sender, EventArgs e)
        {
            string code = txtSourceLib.Text;
            string mainBlock = txtMain.Text;

            CompilerEngine compiler = new CompilerEngine(code, mainBlock);
            lblOutput.Text = compiler.EndToEndCompileAndRun();


        }
    }
}