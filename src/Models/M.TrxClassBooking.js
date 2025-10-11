const { getPool, sql } = require('../Config/db');

async function findAll() {
  const pool = await getPool();

  const result = await pool.request()
    .query(`
      SELECT *
      FROM TrxClassBooking
      WHERE ClassBookingDate BETWEEN 
            DATEADD(MONTH, -1, GETDATE())  -- 2 bulan ke belakang
        AND DATEADD(MONTH, 2, GETDATE())   -- 1 bulan ke depan
      ORDER BY ClassBookingDate DESC
    `);

  return result.recordset;
}

module.exports = { findAll };
