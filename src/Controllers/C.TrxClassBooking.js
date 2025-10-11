const BookingModel = require('../Models/M.TrxClassBooking');

async function index(req, res) {
    const bookings = await BookingModel.findAll();
    res.json(bookings);
}

module.exports = { index };
