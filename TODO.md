# TODO: Add Activation Filtering by Email, Phone, NoIdentity, BirthDate

- [x] Update M.Activation.js: Modify findAll() to accept optional params (email, phone, noIdentity, birthDate) and build dynamic WHERE clause for filtering.
- [x] Update C.Activation.js: Modify index() to extract query params from req.query and pass to findAll().
- [ ] Test the GET /api/activation endpoint with various query params to ensure filtering works correctly.
