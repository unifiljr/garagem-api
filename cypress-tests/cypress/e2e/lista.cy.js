describe('Lista de Veículos', () => {

  beforeEach(() => {
    cy.visit('/lista.html');
  });

  it('deve carregar e exibir a tabela de veículos', () => {
    cy.get('table').should('be.visible');
    cy.get('#lista').should('exist');
  });

  it('deve filtrar os veículos pela busca de marca/modelo', () => {
    // Cadastra um veículo com nome único pra garantir que o teste não dependa
    // de dados que já existam no banco
    cy.visit('/cadastro.html');
    cy.get('#marca').type('MarcaTeste123');
    cy.get('#modelo').type('ModeloTeste123');
    cy.get('#ano').type('2021');
    cy.get('#preco').type('50000');
    cy.contains('button', 'Salvar').click();

    cy.url().should('include', 'lista.html');

    // Busca por um termo que não bate com o veículo cadastrado
    cy.get('input[placeholder="Buscar por marca ou modelo"]').type('XPTO_NAO_EXISTE');
    cy.contains('td', 'MarcaTeste123').should('not.exist');

    // Limpa a busca e digita o termo certo
    cy.get('input[placeholder="Buscar por marca ou modelo"]').clear().type('MarcaTeste123');
    cy.contains('td', 'MarcaTeste123').should('be.visible');
  });

  it('deve excluir um veículo da lista', () => {
    // Cadastra um veículo específico para exclusão
    cy.visit('/cadastro.html');
    cy.get('#marca').type('ParaExcluir');
    cy.get('#modelo').type('Teste');
    cy.get('#ano').type('2020');
    cy.get('#preco').type('10000');
    cy.contains('button', 'Salvar').click();

    cy.url().should('include', 'lista.html');
    cy.contains('td', 'ParaExcluir').should('be.visible');

    // Intercepta o confirm() do navegador para sempre aceitar a exclusão
    cy.on('window:confirm', () => true);

    // Clica no botão Excluir da linha correspondente
    cy.contains('tr', 'ParaExcluir').within(() => {
      cy.contains('button', 'Excluir').click();
    });

    cy.contains('td', 'ParaExcluir').should('not.exist');
  });

});
