DO $$
BEGIN
    -- Verifica e cria a tabela motorcycle
    IF NOT EXISTS (
        SELECT 1 
        FROM information_schema.tables 
        WHERE table_name = 'motorcycle'
    ) THEN
        CREATE TABLE motorcycle (
            identifier VARCHAR(100),
            licence_plate VARCHAR(100),
            model VARCHAR(100),
            year INT
        );
        RAISE NOTICE 'Tabela motorcycle criada com sucesso.';
    ELSE
        RAISE NOTICE 'A tabela motorcycle já existe.';
    END IF;

    -- Verifica e cria a tabela motorcycle_event
    IF NOT EXISTS (
        SELECT 1 
        FROM information_schema.tables 
        WHERE table_name = 'motorcycle_event'
    ) THEN
        CREATE TABLE motorcycle_event (
            id VARCHAR(100),
            motorcycle_id VARCHAR(100),
            licence_plate VARCHAR(100),
            model VARCHAR(100),
            year INT,
            type VARCHAR(100),
            created_at TIMESTAMP
        );
        RAISE NOTICE 'Tabela motorcycle_event criada com sucesso.';
    ELSE
        RAISE NOTICE 'A tabela motorcycle_event já existe.';
    END IF;

    -- Verifica e cria a tabela delivery_person
    IF NOT EXISTS (
        SELECT 1 
        FROM information_schema.tables 
        WHERE table_name = 'delivery_person'
    ) THEN
        CREATE TABLE delivery_person (
            id VARCHAR(100),
            name VARCHAR(200),
            cnpj VARCHAR(200),
            date_of_birth DATE,
            document_number VARCHAR(100),
            document_type VARCHAR(2),
            document_image VARCHAR(1000)
        );
        RAISE NOTICE 'Tabela delivery_person criada com sucesso.';
    ELSE
        RAISE NOTICE 'A tabela delivery_person já existe.';
    END IF;

    -- Verifica e cria a tabela rent
    IF NOT EXISTS (
        SELECT 1 
        FROM information_schema.tables 
        WHERE table_name = 'rent'
    ) THEN
        CREATE TABLE rent (
            id VARCHAR(100),
            delivery_person_id VARCHAR(100),
            motorcycle_id VARCHAR(100),
            start DATE,
            finish DATE,
            end_forecast DATE,
            plan INT
        );
        RAISE NOTICE 'Tabela rent criada com sucesso.';
    ELSE
        RAISE NOTICE 'A tabela rent já existe.';
    END IF;

END $$;

