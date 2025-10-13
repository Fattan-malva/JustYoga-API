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

async function findByUniqCode(uniqCode) {
  const parts = uniqCode.split('|');
  if (parts.length !== 5) {
    throw new Error('Invalid UniqCode format. Expected: studioID|RoomType|ClassID|YYYY-MM-DD|HH:mm');
  }

  const [studioIDStr, roomTypeStr, classIDStr, dateStr, timeStr] = parts;

  const studioID = parseInt(studioIDStr, 10);
  const roomType = parseInt(roomTypeStr, 10);
  const classID = parseInt(classIDStr, 10);

  const pool = await getPool();
  const result = await pool.request()
    .input('studioID', sql.Int, studioID)
    .input('roomType', sql.Int, roomType)
    .input('classID', sql.Int, classID)
    .input('date', sql.VarChar, dateStr)
    .input('time', sql.VarChar, timeStr)
    .query(`
      SELECT TOP 1 *
      FROM TrxClassBooking
      WHERE studioID = @studioID
        AND RoomType = @roomType
        AND ClassID = @classID
        AND CONVERT(varchar(10), ClassBookingDate, 23) = @date
        AND ClassBookingTime = @time
    `);

  return result.recordset;
}

module.exports = { findAll, findByUniqCode };
