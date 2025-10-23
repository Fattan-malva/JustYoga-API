const express = require('express');
const router = express.Router();
const ProductController = require('../Controllers/C.MstProduct');


router.get('/plan-history', ProductController.findPlanProductByCustomerID);
router.get('/just-me-history', ProductController.findJustMeHistoryByCustomerID);

module.exports = router;
