const BookingModel = require('../Models/M.TrxClassBooking');

async function index(req, res) {
    const bookings = await BookingModel.findAll();
    res.json(bookings);
}

async function findByUniqCode(req, res) {
    try {
        const { UniqCode } = req.query;

        if (!UniqCode) {
            return res.status(400).json({ message: "UniqCode is required" });
        }

        const data = await BookingModel.findByUniqCode(UniqCode);

        if (data.length === 0) {
            return res.status(404).json({ message: "No booking found for this UniqCode" });
        }

        res.json(data);
    } catch (error) {
        console.error("Error fetching booking by UniqCode:", error);
        res.status(500).json({ message: "Internal server error" });
    }
}

module.exports = { index, findByUniqCode };
