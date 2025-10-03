const express = require('express');
const router = express.Router();
const ProductController = require('../Controllers/C.product');
const { authenticateToken } = require('../Auth/middleware');

// Public (tidak perlu login)
router.get('/', ProductController.index);
router.get('/:id', ProductController.show);

// Private (harus login pakai JWT)
router.post('/', authenticateToken, ProductController.store);
router.put('/:id', authenticateToken, ProductController.update);
router.delete('/:id', authenticateToken, ProductController.destroy);

module.exports = router;
