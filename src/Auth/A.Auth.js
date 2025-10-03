const bcrypt = require('bcryptjs');
const jwt = require('jsonwebtoken');
const { getPool } = require('../Config/db'); // sesuaikan dengan koneksi SQL Server

// Register User
async function register(req, res) {
  const { username, password } = req.body;
  if (!username || !password) return res.status(400).json({ message: 'Username & password required' });

  const pool = await getPool();
  const existingUser = await pool.request()
    .input('username', username)
    .query('SELECT * FROM Users WHERE username = @username');
  if (existingUser.recordset.length > 0) {
    return res.status(400).json({ message: 'Username already exists' });
  }

  const hashedPassword = await bcrypt.hash(password, 10);
  await pool.request()
    .input('username', username)
    .input('password', hashedPassword)
    .query('INSERT INTO Users (username, password) VALUES (@username, @password)');
  res.status(201).json({ message: 'User registered' });
}

// Login User
async function login(req, res) {
  const { username, password } = req.body;
  const pool = await getPool();
  const result = await pool.request()
    .input('username', username)
    .query('SELECT * FROM Users WHERE username = @username');
  const user = result.recordset[0];
  if (!user) return res.status(400).json({ message: 'Invalid credentials' });

  const match = await bcrypt.compare(password, user.password);
  if (!match) return res.status(400).json({ message: 'Invalid credentials' });

  const token = jwt.sign({ id: user.id, username: user.username }, process.env.JWT_SECRET, { expiresIn: '1h' });
  res.json({ token });
}

module.exports = { register, login };
