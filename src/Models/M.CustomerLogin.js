const { getPool, sql } = require('../Config/db');

async function findById(customerID) {
  const pool = await getPool();
  const result = await pool.request()
    .input('customerID', sql.Int, customerID)
    .query(`SELECT TOP (1000) [customerID]
      ,[name]
      ,[email]
      ,[password]
      ,[toStudioID]
      ,[lastContractID]
      ,[noIdentity]
      ,[birthDate]
      ,[phone]
  FROM [JYISNG].[dbo].[MstCustomerLogin] WHERE customerID = @customerID`);
  return result.recordset[0];
}

module.exports = { findById };
