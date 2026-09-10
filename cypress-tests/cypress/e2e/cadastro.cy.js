describe('Cadastro de Veículo', () => {

  beforeEach(() => {
    cy.visit('/cadastro.html');
  });

  it('deve exibir o formulário com todos os campos', () => {
    cy.get('#marca').should('be.visible');
    cy.get('#modelo').should('be.visible');
    cy.get('#ano').should('be.visible');
    cy.get('#preco').should('be.visible');
    cy.get('#status').should('be.visible');
  });

  it('deve cadastrar um veículo com sucesso e redirecionar para a lista', () => {
    cy.get('#marca').type('Toyota');
    cy.get('#modelo').type('Corolla');
    cy.get('#ano').type('2022');
    cy.get('#preco').type('120000');
    cy.get('#status').select('Disponível');

    cy.contains('button', 'Salvar').click();

    // Após salvar, a tela redireciona para lista.html
    cy.url().should('include', 'lista.html');

    // O carro recém-cadastrado deve aparecer na tabela
    cy.contains('td', 'Toyota').should('be.visible');
    cy.contains('td', 'Corolla').should('be.visible');
  });

  it('deve voltar para a lista ao clicar em Cancelar sem salvar nada', () => {
    cy.get('#marca').type('Nao Deve Salvar');
    cy.contains('button', 'Cancelar').click();

    cy.url().should('include', 'lista.html');
    cy.contains('td', 'Nao Deve Salvar').should('not.exist');
  });

});
