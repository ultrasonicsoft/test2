using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Session11_1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //add code for loading from db
                try
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["CTS_BFS_BatchConnectionString"]
                        .ConnectionString;
                    var connection = new SqlConnection(connectionString);
                    var selectQuery = "SELECT * FROM [USERS]";
                    var selectCommand = new SqlCommand(selectQuery, connection);
                    var dataSet = new DataSet();
                    var adapter = new SqlDataAdapter();
                    adapter.SelectCommand = selectCommand;

                    //Build SQLCommandBuilder object from Adapter
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                    //Connetion is set => Read db  => fill DataSet
                    adapter.Fill(dataSet, "Users");

                    //Bind table data to GridView
                    gvUsers.DataSource = dataSet.Tables["Users"].DefaultView;
                    gvUsers.DataBind();

                    //Add new row
//                    var newRow = dataSet.Tables["Users"].NewRow();
//                    newRow["FirstName"] = "FirstName1";
//                    newRow["LastName"] = "LastName1";
//                    dataSet.Tables["Users"].Rows.Add(newRow);
//
//                    adapter.InsertCommand = builder.GetInsertCommand();
//                    adapter.Update(dataSet, "Users");

                    //Update command
//                    var targetRow = dataSet.Tables[0].Rows[2];
//                    targetRow["FirstName"] = "Varun-Updated";
//                    targetRow["LastName"] = "Jain-Updated";
//                    adapter.UpdateCommand = builder.GetUpdateCommand();
//                    adapter.Update(dataSet, "Users");

                    //Delete Row
//                    var rowToDelete = dataSet.Tables[0].Rows[6];
//                    rowToDelete.Delete();
//                    adapter.DeleteCommand = builder.GetDeleteCommand();
//                    adapter.Update(dataSet, "Users");

                    //Connected Approach - Reader
//                    var query = "SELECT * FROM USERS";
//                    var command = new SqlCommand(query, connection);
//                    connection.Open();
//                    var reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        var firstName = reader["FirstName"].ToString();
//                        var LastName = reader["LastName"].ToString();
//                    }
//                    connection.Close();

                    //Connected Approach - Update/INSERT/Delete sql queries
                    var query = "DELETE FROM Users where ID=4";
                    var command = new SqlCommand(query, connection);
                    connection.Open();
                    var recordBeenUpdated = command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
                
            }
        }
    }
}