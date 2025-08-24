-- Drop tables if exist
DROP TABLE IF EXISTS orders CASCADE;
DROP TABLE IF EXISTS main_menu CASCADE;
DROP TABLE IF EXISTS lib_mode_of_payment CASCADE;
DROP TABLE IF EXISTS expenses CASCADE;
DROP TABLE IF EXISTS adds_on CASCADE;

-- Table: adds_on
CREATE TABLE adds_on (
    adds_on_id SERIAL PRIMARY KEY,
    add_ons VARCHAR(100) NOT NULL,
    price NUMERIC(12,2) NOT NULL
);

-- Table: expenses
CREATE TABLE expenses (
    expense_id SERIAL PRIMARY KEY,
    expense_date TIMESTAMP NOT NULL DEFAULT now(),
    category VARCHAR(50) NOT NULL,
    description VARCHAR(200),
    amount NUMERIC(12,2) NOT NULL
);

-- Table: lib_mode_of_payment
CREATE TABLE lib_mode_of_payment (
    mop_id SERIAL PRIMARY KEY,
    mop VARCHAR(50) NOT NULL UNIQUE
);

-- Table: main_menu
CREATE TABLE main_menu (
    menu_id SERIAL PRIMARY KEY,
    menu VARCHAR(100) NOT NULL UNIQUE,
    price NUMERIC(12,2) NOT NULL
);

-- Table: orders
CREATE TABLE orders (
    order_id SERIAL PRIMARY KEY,
    batch VARCHAR(50),
    name_customer VARCHAR(50),
    menu_order INT NOT NULL,
    add_ons INT,
    quantity INT NOT NULL DEFAULT 1,
    care_of VARCHAR(50),
    payment_scheme INT NOT NULL,
    status VARCHAR(50) NOT NULL DEFAULT 'Pending',
    date_delivered TIMESTAMP,
    date_paid TIMESTAMP,
    created_at TIMESTAMP NOT NULL DEFAULT now(),
    CONSTRAINT fk_orders_addon FOREIGN KEY (add_ons) REFERENCES adds_on(adds_on_id),
    CONSTRAINT fk_orders_menu FOREIGN KEY (menu_order) REFERENCES main_menu(menu_id),
    CONSTRAINT fk_orders_mop FOREIGN KEY (payment_scheme) REFERENCES lib_mode_of_payment(mop_id)
);

-- Insert seed data
INSERT INTO adds_on (adds_on_id, add_ons, price) VALUES (1, 'Kimchi', 5.00);
INSERT INTO adds_on (adds_on_id, add_ons, price) VALUES (2, 'Spam', 30.00);
INSERT INTO adds_on (adds_on_id, add_ons, price) VALUES (3, 'Cheese', 5.00);

-- Reset sequence since we inserted IDs manually
SELECT setval('adds_on_adds_on_id_seq', (SELECT MAX(adds_on_id) FROM adds_on));
