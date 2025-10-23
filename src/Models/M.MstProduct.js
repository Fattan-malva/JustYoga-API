const { getPool, sql } = require('../Config/db');

async function findPlanProductByCustomerID(customerID) {
  const pool = await getPool();

  const result = await pool.request()
    .input('customerID', sql.VarChar, customerID)
    .query(`
      SELECT 
        mp.productName, 
        tc.startDate, 
        tc.endDate,
        tc.trxDate
      FROM TrxContract tc
      INNER JOIN MstProduct mp ON tc.productID = mp.productID
      WHERE tc.customerID = @customerID
      ORDER BY tc.trxDate ASC
    `);

  return result.recordset;
}

module.exports = { findPlanProductByCustomerID };
