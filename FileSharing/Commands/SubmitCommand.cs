
using FileSharing.CommandBaseSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileSharing.Commands
{
    public class SubmitCommand : CommandBase
    {
        public string domain;
        public SubmitCommand(string domain) { 
            this.domain = domain;
        }
        public override void Execute(object parameter)
        {
            MessageBox.Show(domain.ToString());
        }
    }
}
