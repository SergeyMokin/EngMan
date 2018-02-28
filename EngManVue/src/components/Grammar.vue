<template>
    <div class = "view-list">
        <div class = "icon-close"><router-link to="/"><img src = "../assets/arrow-up.png" title="Назад" style = "margin: 5px; width: 20px; height: 20px;"></router-link></div>
        <router-link to="/grammar/rules" class = "routes-admin">Rules</router-link>
        <router-link to="/grammar/sentencetask" class = "routes-admin">Practice rules</router-link>
    </div>
</template>

<style scoped>
*{
    font-size: 25px;
}
</style>