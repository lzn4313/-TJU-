using System.Data;
using Bank_database_system;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
namespace Bank_database_system
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            ConnectDB connectDB = new ConnectDB();
            string query = "SELECT * FROM TODOITEM";

            try
            {
                OracleDataReader reader = connectDB.Read(query);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader); // �������ݵ�DataTable
                dataGridView1.DataSource = dataTable; // ��DataTable��DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                connectDB.DBClose(); // ȷ���ر����ݿ�����
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
