const ClassModel = require('../Models/M.MstRoomType');

async function index(req, res) {
    const classes = await ClassModel.findAll();
    res.json(classes);
}

module.exports = { index };
