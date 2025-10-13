const express = require('express');
const router = express.Router();
const BookingController = require('../Controllers/C.TrxClassBooking');

router.get('/', BookingController.index);
router.get('/find-by-uniq-code', BookingController.findByUniqCode);

module.exports = router;
