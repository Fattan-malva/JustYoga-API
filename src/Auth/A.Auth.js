const bcrypt = require('bcryptjs');
const jwt = require('jsonwebtoken');
const { getPool } = require('../Config/db'); // koneksi SQL Server

// Register User
async function register(req, res) {
  const { name, email, password } = req.body;

  if (!name || !email || !password) {
    return res.status(400).json({ message: 'Name, email, and password are required' });
  }

  try {
    const pool = await getPool();

    // Cek apakah email sudah terdaftar
    const existingUser = await pool.request()
      .input('email', email)
      .query('SELECT * FROM MstCustomerLogin WHERE email = @email');

    if (existingUser.recordset.length > 0) {
      return res.status(400).json({ message: 'Email already registered' });
    }

    // Hash password
    const hashedPassword = await bcrypt.hash(password, 10);

    // Generate manual customerID (misal timestamp atau random)
    const customerID = Date.now().toString(); // contoh sederhana

    // Simpan user baru
    await pool.request()
      .input('customerID', customerID)
      .input('name', name)
      .input('email', email)
      .input('password', hashedPassword)
      .query(`
        INSERT INTO MstCustomerLogin (customerID, name, email, password)
        VALUES (@customerID, @name, @email, @password)
      `);

    res.status(201).json({ message: 'User registered successfully' });

  } catch (error) {
    console.error('Register error:', error);
    res.status(500).json({ message: 'Internal server error' });
  }
}

// Login User (by email)
async function login(req, res) {
  const { email, password } = req.body;

  if (!email || !password) {
    return res.status(400).json({ message: 'Email and password are required' });
  }

  try {
    const pool = await getPool();
    const result = await pool.request()
      .input('email', email)
      .query('SELECT * FROM MstCustomerLogin WHERE email = @email');

    const user = result.recordset[0];
    if (!user) {
      return res.status(400).json({ message: 'Invalid email or password' });
    }

    const match = await bcrypt.compare(password, user.password);
    if (!match) {
      return res.status(400).json({ message: 'Invalid email or password' });
    }

    // Generate JWT token
    const token = jwt.sign(
      { id: user.customerID, email: user.email, name: user.name },
      process.env.JWT_SECRET,
      { expiresIn: '1h' }
    );

    res.json({
      message: 'Login successful',
      token,
      user: {
        customerID: user.customerID,
        name: user.name,
        email: user.email
      }
    });

  } catch (error) {
    console.error('Login error:', error);
    res.status(500).json({ message: 'Internal server error' });
  }
}

module.exports = { register, login };
