# EmployeeProductivity
---
#Цель:
Оптимизировать учёт работы сотрудников.
#Ожидаемый функционал:
1. Разделение пользователей на роли
    а. Директор - может создавать отделы для разделения рабочих, задания, выставлять им приоритет за счёт баллов, которые начислятся за выполнение.
    б. Работник - может присоединиться к существующему отделу, выполнять задания отдела.
2. Рэйтингиовая система:
    а. За выполнение заданий выдаётся награда (баллы).
    б. Должна быть возможность посмотреть на статистику баллов определённого работника/отдела за день/неделю/месяц.
---
#В проекте пока не реализованно:
1. На данный момент нет полной связи между бэкендом и фронтендом.
2. Директор не может добавлять не зарегистрированных пользователей, в отдела (однако, он может работать с заранее зарегистрированных пользователей)
3. На данный момент данные для рэйтинга генерируются автоматически на стороне фронтенда.