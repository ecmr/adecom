using AdECon.Model;
using AdECon.Dal;
using System;


namespace AdECon.Bus
{
    public class EncomendaBus
    {
        public Morador Consultar(int idMorador)
        {
            return DalHelper.GetCliente(idMorador);
        }

        public bool Adicionar(Morador morador)
        {
            return DalHelper.Add(morador);
        }

        public bool Atualizar(Morador morador)
        {
            return DalHelper.Update(morador);
        }

        public bool Excluir(int idMorador)
        {
            return DalHelper.Delete(idMorador);
        }
    }
}
