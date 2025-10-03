const Product = require('../Models/M.product');

async function index(req, res) {
  const products = await Product.findAll();
  res.json(products);
}

async function show(req, res) {
  const id = Number(req.params.id);
  const product = await Product.findById(id);
  if (!product) return res.status(404).json({ message: 'Not found' });
  res.json(product);
}

async function store(req, res) {
  const { name, price, stock } = req.body;

  if (!name || !price || !stock) {
    return res.status(400).json({ message: 'Name, price, and stock are required' });
  }

  const product = await Product.create(req.body);
  res.status(201).json(product);
}

async function update(req, res) {
  const id = Number(req.params.id);
  const { name, price, stock } = req.body;

  if (!name || !price || !stock) {
    return res.status(400).json({ message: 'Name, price, and stock are required' });
  }

  const product = await Product.update(id, req.body);
  if (!product) return res.status(404).json({ message: 'Not found' });
  res.json(product);
}

async function destroy(req, res) {
  const id = Number(req.params.id);
  await Product.remove(id);
  res.status(204).send();
}

module.exports = { index, show, store, update, destroy };
