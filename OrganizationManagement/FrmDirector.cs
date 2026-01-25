using System.Windows.Forms;

namespace OrganizationManagement
{
    public partial class FrmDirector : Form
    {
        private int orgId;

        public FrmDirector(int orgId)
        {
            InitializeComponent();
            this.orgId = orgId;
        }

        private void FrmDirector_Load(object sender, System.EventArgs e)
        {

        }
    }
}
