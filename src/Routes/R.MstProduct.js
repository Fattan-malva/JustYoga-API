const express = require('express');
const router = express.Router();
const ProductController = require('../Controllers/C.MstProduct');


router.get('/plan-history', ProductController.findPlanProductByCustomerID);

module.exports = router;
