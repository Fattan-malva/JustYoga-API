const JustMeModel = require('../Models/M.TrxTchJM_Available');

async function getAll(req, res) {
  try {
    const justme = await JustMeModel.findAll();
    res.json(justme);
  } catch (error) {
    console.error("Error fetching all justme:", error);
    res.status(500).json({ message: "Internal server error" });
  }
}

async function getByDate(req, res) {
  try {
    const { date } = req.query;

    if (!date) {
      return res.status(400).json({ message: "date is required" });
    }

    const justme = await JustMeModel.findByDate(date);

    if (justme.length === 0) {
      return res.status(404).json({ message: "No data found for this date" });
    }

    res.json(justme);
  } catch (error) {
    console.error("Error fetching justme by date:", error);
    res.status(500).json({ message: "Internal server error" });
  }
}

module.exports = { getAll, getByDate };
