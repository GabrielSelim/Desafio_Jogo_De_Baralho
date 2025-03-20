# Use a imagem base do Node.js
FROM node:18 AS build

# Defina o diretório de trabalho no contêiner
WORKDIR /app

# Copie os arquivos de projeto e instale as dependências
COPY package*.json ./
RUN npm install

# Copie o restante dos arquivos e construa o projeto
COPY . .
RUN npm run build --prod

# Use a imagem base do Nginx para servir o front-end
FROM nginx:alpine
COPY --from=build /app/dist/desafio-jogo-de-baralho-frontend /usr/share/nginx/html

# Exponha a porta 80
EXPOSE 80

# Comando de entrada para iniciar o Nginx
CMD ["nginx", "-g", "daemon off;"]