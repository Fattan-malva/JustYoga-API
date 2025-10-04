const express = require('express');
const router = express.Router();
const ScheduleController = require('../Controllers/C.TrxSchedule');

// GET schedule berdasarkan parameter (date, roomType, studioID)
router.get('/', ScheduleController.getSchedule);
// GET schedule berdasarkan date saja
router.get('/by-date', ScheduleController.getScheduleByDate);
// GET schedule berdasarkan date dan studio saja
router.get('/by-date-studio', ScheduleController.getScheduleByDateAndStudio);

module.exports = router;
