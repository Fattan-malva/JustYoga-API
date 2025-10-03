const { getPool, sql } = require('../Config/db');

async function findAll() {
    const pool = await getPool();
    const result = await pool.request().query('SELECT * FROM MstStudio');
    return result.recordset;
}
module.exports = { findAll };
