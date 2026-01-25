using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Windows.Forms;

namespace OrganizationManagement
{
    public partial class FrmOrganization : Form
    {
        public FrmOrganization()
        {
            InitializeComponent();
        }

        private void lblPhone_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                if (!txtPhone.Text.All(char.IsDigit) ||
                    txtPhone.Text.Length < 9 ||
                    txtPhone.Text.Length > 12)
                {
                    MessageBox.Show("Phone must be 9-12 digits");
                    return;
                }
            }

        }

        private void lblOrgName_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOrgName.Text))
            {
                MessageBox.Show("Organization Name is required");
                return;
            }

            if (txtOrgName.Text.Trim().Length < 3 || txtOrgName.Text.Trim().Length > 255)
            {
                MessageBox.Show("Organization Name must be between 3 and 255 characters");
                return;
            }

        }

        private void lblEmail_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                try
                {
                    var mail = new System.Net.Mail.MailAddress(txtEmail.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid email format");
                    return;
                }
            }

        }

        private int savedOrgId = 0;
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (IsOrgNameExists(txtOrgName.Text))
            {
                MessageBox.Show("Organization Name already exists");
                return;
            }

            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO ORGANIZATION
        (OrgName, Address, Phone, Email, CreatedDate)
        VALUES (@name, @address, @phone, @email, @created)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", txtOrgName.Text.Trim());
                cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@created", DateTime.Now);

                cmd.ExecuteNonQuery();
                savedOrgId = (int)cmd.LastInsertedId;
            }

            MessageBox.Show("Save successfully");
            btnDirector.Enabled = true;
        }
        private bool IsOrgNameExists(string orgName)
        {
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM ORGANIZATION WHERE OrgName = @name";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", orgName.Trim());

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private void btnDirector_Click(object sender, EventArgs e)
        {
            FrmDirector frm = new FrmDirector(savedOrgId);
            frm.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
