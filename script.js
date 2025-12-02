// Dataset com 5000 registros simulados
const dataset = Array.from({ length: 5000 }, (_, i) => ({
  id: i + 1,
  pergunta: `Pergunta exemplo número ${i + 1}`,
  resposta: `Resposta gerada automaticamente para a pergunta número ${i + 1}.`,
  data: new Date().toISOString()
}));

// Arrays de registros
const requisicoes = [];
const uteis = [];

// Gera resposta e mostra na tela
function gerarResposta() {
  const input = document.getElementById("inputPergunta").value.trim();
  const respostaBox = document.getElementById("resposta");

  if (!input) {
    respostaBox.innerHTML = "<p>⚠️ Por favor, digite uma pergunta.</p>";
    return;
  }

  const index = Math.floor(Math.random() * dataset.length);
  const resposta = dataset[index].resposta;

  respostaBox.innerHTML = `
    <p><strong>IA:</strong> ${resposta}</p>
    <div class="feedback">
      <p>Essa resposta foi útil?</p>
      <button onclick="registrarFeedback('${input.replace(/'/g, "\\'")}', '${resposta.replace(/'/g, "\\'")}', true)">✅ Sim</button>
      <button onclick="registrarFeedback('${input.replace(/'/g, "\\'")}', '${resposta.replace(/'/g, "\\'")}', false)">❌ Não</button>
    </div>
  `;

  requisicoes.push({ pergunta: input, resposta, util: null, data: new Date().toLocaleString() });
  salvarCSV('requisicoes.csv', requisicoes);
}

// Registra feedback e salva arquivos
function registrarFeedback(pergunta, resposta, util) {
  const registro = { pergunta, resposta, util, data: new Date().toLocaleString() };

  const index = requisicoes.findIndex(r => r.pergunta === pergunta);
  if (index !== -1) requisicoes[index].util = util;

  if (util) {
    uteis.push(registro);
    salvarCSV('uteis.csv', uteis);
  }

  alert("✅ Feedback registrado com sucesso!");
}

// Salva os dados em CSV
function salvarCSV(nomeArquivo, dados) {
  const cabecalho = "Pergunta,Resposta,Útil,Data\n";
  const linhas = dados.map(d =>
    `"${d.pergunta.replace(/"/g, '""')}","${d.resposta.replace(/"/g, '""')}",${d.util},${d.data}`
  ).join("\n");

  const blob = new Blob([cabecalho + linhas], { type: "text/csv;charset=utf-8;" });
  const link = document.createElement("a");
  link.href = URL.createObjectURL(blob);
  link.download = nomeArquivo;
  link.click();
}