using DirectoryToolsExamples.ViewModel;
using System.Windows.Controls;

namespace DirectoryToolsExamples.View
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class XplorerView : UserControl
    {
        public XplorerView()
        {
            DataContext = new XplorerViewModel();
            InitializeComponent();
        }
    }
}
