using AutoMapper;
using api.Senhas;
using api.Usuarios;
using app.Entidades;
using api;
using EnumsNET;
using api.Perfis;
using api.Permissoes;

namespace app.Services.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dto => dto.CNPJ, opt => opt.MapFrom(u => u.Empresas.FirstOrDefault().Cnpj));

            CreateMap<UsuarioDnit, Usuario>()
                .ForMember(u => u.RedefinicaoSenha, opt => opt.Ignore())
                .ForMember(u => u.Empresas, opt => opt.Ignore())
                .ForMember(u => u.Perfil, opt => opt.Ignore())
                .ForMember(u => u.PerfilId, opt => opt.Ignore())
                .ForMember(u => u.TokenAtualizacao, opt => opt.Ignore())
                .ForMember(u => u.TokenAtualizacaoExpiracao, opt => opt.Ignore());

            CreateMap<UsuarioModel, UsuarioDTO>()
                .ForMember(dto => dto.CNPJ, opt => opt.Ignore())
                .ForMember(dto => dto.UfLotacao, opt => opt.Ignore());
                
            CreateMap<Usuario, UsuarioModel>()
                .ForMember(dto => dto.Cnpj, opt => opt.MapFrom(u => u.Empresas.FirstOrDefault().Cnpj));

            CreateMap<UF, UfModel>()
                .ForMember(model => model.Id, opt => opt.MapFrom(uf => (int)uf))
                .ForMember(model => model.Sigla, opt => opt.MapFrom(uf => uf.ToString()))
                .ForMember(model => model.Nome, opt => opt.MapFrom(uf => uf.AsString(EnumFormat.Description)));

            CreateMap<UsuarioDTO, UsuarioDnit>()
                .ForMember(usuarioDnit => usuarioDnit.Id, opt => opt.Ignore());

            CreateMap<RedefinicaoSenhaDTO, RedefinicaoSenhaModel>();

            CreateMap<UsuarioDTO, UsuarioTerceiro>()
                .ForMember(usuarioTerceiro => usuarioTerceiro.Id, opt => opt.Ignore());

            CreateMap<PerfilDTO, Perfil>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.Permissoes, opt => opt.Ignore())
                .ForMember(p => p.PerfilPermissoes, opt => opt.Ignore())
                .ForMember(p => p.Usuarios, opt => opt.Ignore())
                .ForMember(p => p.Tipo, opt => opt.Ignore())
                .ForMember(p => p.PermissoesSessao, opt => opt.Ignore());
            
            CreateMap<Usuario, UsuarioModelNovo>()
                .ForMember(u => u.Cnpj, opt => opt.Ignore());

            CreateMap<Perfil, PerfilModel>()
                .ForMember(model => model.Permissoes, opt => opt.MapFrom
                    (
                        perf => perf.Permissoes.Select(p => new PermissaoModel
                            {
                                Codigo = p,
                                Descricao = p.AsString(EnumFormat.Description)!
                            }).ToList()
                    )
                )
                .ForMember(model => model.QuantidadeUsuarios, opt => opt.MapFrom(p => p.Usuarios.Count()))
                .ForMember(model => model.CategoriasPermissao, opt => opt.Ignore());
        }
    }
}