<template>
    <div class = "view-list">
        <div class = "icon-close"><router-link to="/"><img src = "../assets/arrow-up.png" title="Назад" style = "margin: 5px; width: 20px; height: 20px;"></router-link></div>
        <router-link to="/trainings/word_translate_task" class = "routes-admin">Word-translate</router-link>
        <router-link to="/trainings/translate_word_task" class = "routes-admin">Translate-word</router-link>
        <router-link to="/trainings/guessestheimages" class = "routes-admin">Guess what's on the picture</router-link>
    </div>
</template>

<style scoped>
*{
    font-size: 25px;
}
</style>