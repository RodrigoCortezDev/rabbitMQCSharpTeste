using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfRabbitMQ.Postgres
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class ListaComponente
    {
        public string razaoSocial { get; set; }
        public int codigoComponente { get; set; }
        public string cnpjRaiz { get; set; }
        public string dvCNPJRaiz { get; set; }
        public int codigoPontoVenda { get; set; }
        public string razaoSocialPontoVenda { get; set; }
        public string endereco { get; set; }
        public string bairro { get; set; }
        public string municipio { get; set; }
        public string siglaUF { get; set; }
        public string cep { get; set; }
        public string nomeContato { get; set; }
        public string dddTelefone { get; set; }
        public string telefone { get; set; }
        public string dddFax { get; set; }
        public string fax { get; set; }
        public string imagemPosto { get; set; }
        public string inscricaoEstadual { get; set; }
        public object inscricaoMunicipal { get; set; }
        public int codigoInternoAtividadeNegocio { get; set; }
        public string descricaoInternoAtividadeNegocio { get; set; }
        public object codigoInternoTipoNegocio { get; set; }
        public object descricaoInternoTipoNegocio { get; set; }
        public int codigoBandeiraPosto { get; set; }
        public string descricaoBandeiraPosto { get; set; }
        public string propriedadeBandeira { get; set; }
        public int codigoZonaVenda { get; set; }
        public string descricaoZonaVenda { get; set; }
        public string centroCusto { get; set; }
        public string descricaoCentroCusto { get; set; }
        public string codigoTipoComponente { get; set; }
        public string descricaoTipoComponente { get; set; }
        public string tipoPessoa { get; set; }
        public int CNAE { get; set; }
        public string emailContato { get; set; }
        public string an8 { get; set; }
        public string situacao { get; set; }
        public string filialAbastecedora { get; set; }
        public string pontoEntrega { get; set; }
        public string codigoPontoDeVendaAbadi { get; set; }
        public string listaComponenteKey { get; set; }
    }

    public class Corporate
    {
        public object mensagem { get; set; }
        public List<ListaComponente> listaComponente { get; set; }
    }


}
