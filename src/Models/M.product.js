const { getPool, sql } = require('../Config/db');

async function findAll() {
  const pool = await getPool();
  const result = await pool.request().query('SELECT * FROM Products');
  return result.recordset;
}

async function findById(id) {
  const pool = await getPool();
  const result = await pool.request()
    .input('id', sql.Int, id)
    .query('SELECT * FROM Products WHERE Id=@id');
  return result.recordset[0];
}

async function create(data) {
  const pool = await getPool();
  const result = await pool.request()
    .input('Name', sql.NVarChar, data.name)
    .input('Price', sql.Decimal(18, 2), data.price)
    .input('Stock', sql.Int, data.stock)
    .query(`INSERT INTO Products (Name, Price, Stock)
            OUTPUT INSERTED.*
            VALUES (@Name, @Price, @Stock)`);
  return result.recordset[0];
}


async function update(id, data) {
  const pool = await getPool();
  const result = await pool.request()
    .input('id', sql.Int, id)
    .input('Name', sql.NVarChar, data.name)
    .input('Price', sql.Decimal(18,2), data.price)
    .input('Stock', sql.Int, data.stock)
    .query(`UPDATE Products
            SET Name=@Name, Price=@Price, Stock=@Stock
            OUTPUT INSERTED.*
            WHERE Id=@id`);
  return result.recordset[0];
}

async function remove(id) {
  const pool = await getPool();
  await pool.request().input('id', sql.Int, id)
    .query('DELETE FROM Products WHERE Id=@id');
  return;
}

module.exports = { findAll, findById, create, update, remove };
