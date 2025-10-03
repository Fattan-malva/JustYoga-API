const express = require('express');
const router = express.Router();
const ScheduleController = require('../Controllers/C.schedule');

// GET schedule berdasarkan parameter (date, roomType, studioID)
router.get('/', ScheduleController.getSchedule);
// GET schedule berdasarkan date saja
router.get('/by-date', ScheduleController.getScheduleByDate);


module.exports = router;
