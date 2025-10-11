const express = require('express');
const router = express.Router();
const BookingController = require('../Controllers/C.TrxClassBooking');

router.get('/', BookingController.index);

module.exports = router;
