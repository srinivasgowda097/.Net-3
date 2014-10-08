using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using Cshandler.CompilationExecutionEngine;

namespace Cshandler.csfiddle.web
{
    public partial class New : System.Web.UI.Page
    {
        const string HTML_TAG_PATTERN = "<.*?>";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCompileAndRun_Click(object sender, EventArgs e)
        {
            string code = this.StripHTML(txtSourceLib.InnerText);
            string mainBlock = this.StripHTML(txtMain.InnerText);

            CompilerEngine compiler = new CompilerEngine(code, mainBlock);
            lblOutput.Text = compiler.EndToEndCompileAndRun();
        }

        private string StripHTML(string inputString)
        {
            return Regex.Replace
              (inputString, HTML_TAG_PATTERN, string.Empty);
        }
    }
}