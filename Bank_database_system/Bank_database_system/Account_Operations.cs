﻿using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Bank_database_system
{
    /*
     * Account_Operations类中包含账户操作的函数，函数中的所有的MessageBox显示信息都是暂时调试用的
     */
    public static class Account_Operations
    {
        public const string connectionString =
                @"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=47.96.39.153)(PORT=1521))(CONNECT_DATA=(SID=orcl)));
                User Id=system;Password=Tongji123456;";

        // 显示用户查询的结果
        public static void Customer_inquiry_result(string ID)
        {
            DataTable dataTable = Customer_inquiry(ID);
            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("查询失败");
            }
            // 获取字段的值并处理
            foreach (DataRow row in dataTable.Rows)
            {
                MessageBox.Show($" ID: {row["IDENTIFICATION_NUMBER"]}\n" +
                    $" Name: {row["CUSTOMER_NAME"]}\n" +
                    $" Address: {row["CUSTOMER_ADDRESS"]}\n" +
                    $" Phone Number: {row["CUSTOMER_PHONENUMBER"]}");
            }
        }

        // 用户查询的步骤
        private static DataTable Customer_inquiry(string ID)
        {
            // 返回的表
            DataTable dataTable = new DataTable();
            // 创建数据库连接对象
            try
            {
                using (OracleConnection connection = new(connectionString))
                {
                    // 打开数据库连接
                    connection.Open();

                    // 数据库操作
                    string selectQuery = "SELECT * FROM CUSTOMER WHERE IDENTIFICATION_NUMBER = :ID";
                    using (OracleCommand command = new OracleCommand(selectQuery, connection))
                    {
                        OracleParameter param = new OracleParameter(":ID", OracleDbType.Varchar2);
                        param.Value = ID; // 替换成实际的身份证号
                        command.Parameters.Add(param);

                        // 执行查询
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader); // 将查询结果加载到 DataTable
                            return dataTable;
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                // 处理 Oracle 数据库相关异常
                MessageBox.Show($"数据库操作失败：{ex.Message}", "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
            catch (Exception ex)
            {
                // 处理其他类型的异常
                MessageBox.Show($"发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }

        // 用户插入
        public static bool Customer_insert(string ID, string name, string address, string phoneNumber)
        {
            try
            {
                using (OracleConnection connection = new(connectionString))
                {
                    // 打开数据库连接
                    connection.Open();

                    // 数据库操作
                    string insertQuery = "INSERT INTO CUSTOMER (IDENTIFICATION_NUMBER, CUSTOMER_NAME, CUSTOMER_ADDRESS, CUSTOMER_PHONENUMBER) " +
                             "VALUES (:ID, :name, :address, :phoneNumber)";
                    using (OracleCommand command = new OracleCommand(insertQuery, connection))
                    {
                        // 添加参数
                        command.Parameters.Add(":ID", OracleDbType.Varchar2).Value = ID;
                        command.Parameters.Add(":name", OracleDbType.Varchar2).Value = name;
                        command.Parameters.Add(":address", OracleDbType.Varchar2).Value = address;
                        command.Parameters.Add(":phoneNumber", OracleDbType.Varchar2).Value = phoneNumber;

                        // 执行插入操作
                        if (command.ExecuteNonQuery() > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                // 处理 Oracle 数据库相关异常
                MessageBox.Show($"数据库操作失败：{ex.Message}", "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                // 处理其他类型的异常
                MessageBox.Show($"发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // 根据身份证号ID更新用户信息
        public static bool Customer_update(string IDtoUpdate, string newName, string newAddress, string newPhoneNumber)
        {
            try
            {
                using (OracleConnection connection = new(connectionString))
                {
                    // 打开数据库连接
                    connection.Open();

                    // 构建更新语句
                    string updateQuery = @"UPDATE CUSTOMER SET CUSTOMER_NAME = :newName, 
                        CUSTOMER_ADDRESS = :newAddress, 
                        CUSTOMER_PHONENUMBER = :newPhoneNumber
                        WHERE IDENTIFICATION_NUMBER = :IDtoUpdate";
                    using (OracleCommand command = new OracleCommand(updateQuery, connection))
                    {
                        // 添加参数
                        command.Parameters.Add(":newName", OracleDbType.Varchar2).Value = newName;
                        command.Parameters.Add(":newAddress", OracleDbType.Varchar2).Value = newAddress;
                        command.Parameters.Add(":newPhoneNumber", OracleDbType.Varchar2).Value = newPhoneNumber;
                        command.Parameters.Add(":IDtoUpdate", OracleDbType.Varchar2).Value = IDtoUpdate;

                        // 执行更新操作
                        if (command.ExecuteNonQuery() > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                // 处理 Oracle 数据库相关异常
                MessageBox.Show($"数据库操作失败：{ex.Message}", "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                // 处理其他类型的异常
                MessageBox.Show($"发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}
