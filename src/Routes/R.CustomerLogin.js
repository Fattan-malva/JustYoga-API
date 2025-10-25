const express = require('express');
const router = express.Router();
const CustomerLoginController = require('../Controllers/C.CustomerLogin');

// Public route to get customer by ID
router.get('/', CustomerLoginController.show);
router.get('', CustomerLoginController.show);

module.exports = router;
