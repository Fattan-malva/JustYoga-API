const express = require('express');
const router = express.Router();
const JustMeController = require('../Controllers/C.TrxTchJM_Available');

// GET all justme
router.get('/', JustMeController.getAll);
// GET justme by date
router.get('/by-date', JustMeController.getByDate);

module.exports = router;
