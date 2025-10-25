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
      SELECT *
      FROM TrxClassBooking
      WHERE studioID = @studioID
        AND RoomType = @roomType
        AND ClassID = @classID
        AND CONVERT(varchar(10), ClassBookingDate, 23) = @date
        AND ClassBookingTime = @time
    `);

  return result.recordset;
}

async function create(bookingData) {
  const pool = await getPool();

  // Ensure customerID is a string
  bookingData.customerID = String(bookingData.customerID);

  // 🔍 1. Cek apakah user punya booking yang belum dirilis
  const checkExisting = await pool.request()
    .input('customerID', sql.VarChar(255), bookingData.customerID)
    .query(`
      SELECT TOP 1 *
      FROM TrxClassBooking
      WHERE customerID = @customerID AND isRelease = 0
    `);

  // Jika sudah ada booking yang belum diselesaikan
  if (checkExisting.recordset.length > 0) {
    return {
      success: false,
      message: 'Please complete your previous booking before creating a new one.',
    };
  }

  // ✅ 2. Jika tidak ada booking aktif, buat booking baru
  const result = await pool.request()
    .input('studioID', sql.VarChar, bookingData.studioID)
    .input('RoomType', sql.Int, bookingData.RoomType)
    .input('ClassID', sql.Int, bookingData.ClassID)
    .input('ClassBookingDate', sql.DateTime, new Date(bookingData.ClassBookingDate))
    .input('ClassBookingTime', sql.VarChar, bookingData.ClassBookingTime)
    .input('customerID', sql.VarChar(255), bookingData.customerID)
    .input('ContractID', sql.VarChar, bookingData.ContractID)
    .input('AccessCardNumber', sql.Int, bookingData.AccessCardNumber)
    .input('isActive', sql.Bit, bookingData.isActive)
    .input('isRelease', sql.Bit, bookingData.isRelease)
    .input('isConfirm', sql.Bit, bookingData.isConfirm)
    .input('ClassMapNumber', sql.Int, bookingData.ClassMapNumber)
    .input('createby', sql.VarChar, bookingData.createby)
    .input('createdate', sql.DateTime, new Date(bookingData.createdate))
    .query(`
      INSERT INTO TrxClassBooking (
        studioID, RoomType, ClassID, ClassBookingDate, ClassBookingTime,
        customerID, ContractID, AccessCardNumber, isActive, isRelease,
        isConfirm, ClassMapNumber, createby, createdate
      ) VALUES (
        @studioID, @RoomType, @ClassID, @ClassBookingDate, @ClassBookingTime,
        @customerID, @ContractID, @AccessCardNumber, @isActive, @isRelease,
        @isConfirm, @ClassMapNumber, @createby, @createdate
      )
    `);

  return { success: true, message: 'Booking created successfully.' };
}

//NO VALIDATED

// async function create(bookingData) {
//   const pool = await getPool();
//   const result = await pool.request()
//     .input('studioID', sql.VarChar, bookingData.studioID)
//     .input('RoomType', sql.Int, bookingData.RoomType)
//     .input('ClassID', sql.Int, bookingData.ClassID)
//     .input('ClassBookingDate', sql.DateTime, new Date(bookingData.ClassBookingDate))
//     .input('ClassBookingTime', sql.VarChar, bookingData.ClassBookingTime)
//     .input('customerID', sql.VarChar, bookingData.customerID)
//     .input('ContractID', sql.VarChar, bookingData.ContractID)
//     .input('AccessCardNumber', sql.Int, bookingData.AccessCardNumber)
//     .input('isActive', sql.Bit, bookingData.isActive)
//     .input('isRelease', sql.Bit, bookingData.isRelease)
//     .input('isConfirm', sql.Bit, bookingData.isConfirm)
//     .input('ClassMapNumber', sql.Int, bookingData.ClassMapNumber)
//     .input('createby', sql.VarChar, bookingData.createby)
//     .input('createdate', sql.DateTime, new Date(bookingData.createdate))
//     .query(`
//       INSERT INTO TrxClassBooking (
//         studioID, RoomType, ClassID, ClassBookingDate, ClassBookingTime,
//         customerID, ContractID, AccessCardNumber, isActive, isRelease,
//         isConfirm, ClassMapNumber, createby, createdate
//       ) VALUES (
//         @studioID, @RoomType, @ClassID, @ClassBookingDate, @ClassBookingTime,
//         @customerID, @ContractID, @AccessCardNumber, @isActive, @isRelease,
//         @isConfirm, @ClassMapNumber, @createby, @createdate
//       )
//     `);

//   return { success: true };
// }

module.exports = { findAll, findByUniqCode, create };
