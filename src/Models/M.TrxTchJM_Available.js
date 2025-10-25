const { getPool, sql } = require('../Config/db');

async function findAll() {
  const pool = await getPool();

  const query = `
    SELECT
        a.TrxDate,
        a.toStudioID,
        a.TchID,
        e.EmployeeName,
        s.Name AS StudioName,
        a.Sequence,
        a.TimeFrom,
        a.TimeTo,
        a.isBook,
        a.CreatedDate,
        a.CreatedBy
    FROM TrxTchJM_Available AS a
    INNER JOIN MstEmployee AS e
        ON a.TchID = e.EmployeeID
    INNER JOIN MstStudio AS s
        ON a.toStudioID = s.StudioID
    WHERE a.isBook = 'false'
    ORDER BY a.Sequence ASC;
  `;

  const result = await pool.request().query(query);
  return result.recordset;
}

async function findByDate(date) {
  const pool = await getPool();

  const query = `
    SELECT
        a.TrxDate,
        a.toStudioID,
        a.TchID,
        e.EmployeeName,
        s.Name AS StudioName,
        a.Sequence,
        a.TimeFrom,
        a.TimeTo,
        a.isBook,
        a.CreatedDate,
        a.CreatedBy
    FROM TrxTchJM_Available AS a
    INNER JOIN MstEmployee AS e
        ON a.TchID = e.EmployeeID
    INNER JOIN MstStudio AS s
        ON a.toStudioID = s.StudioID
    WHERE a.TrxDate = @date AND a.isBook = 'false'
    ORDER BY a.Sequence ASC;
  `;

  const result = await pool.request().input('date', sql.Date, date).query(query);
  return result.recordset;
}

async function findByDateAndStudio(date, studioID) {
  const pool = await getPool();

  const query = `
    SELECT
        a.TrxDate,
        a.toStudioID,
        a.TchID,
        e.EmployeeName,
        s.Name AS StudioName,
        a.Sequence,
        a.TimeFrom,
        a.TimeTo,
        a.isBook,
        a.CreatedDate,
        a.CreatedBy
    FROM TrxTchJM_Available AS a
    INNER JOIN MstEmployee AS e
        ON a.TchID = e.EmployeeID
    INNER JOIN MstStudio AS s
        ON a.toStudioID = s.StudioID
    WHERE a.TrxDate = @date AND a.toStudioID = @studioID AND a.isBook = 'false'
    ORDER BY a.Sequence ASC;
  `;

  const result = await pool.request().input('date', sql.Date, date).input('studioID', sql.Int, studioID).query(query);
  return result.recordset;
}

module.exports = { findAll, findByDate, findByDateAndStudio };
