# ASM-NewRemoteAdmins
A C# tool to gather the count of administrators on the crown jewel machine and detects when this number changes

The challenge required not to use domain admin / local admin privileges to retreive local admin group members on a domain machine, but a tweak on other permissions. So I tried different permissions and ways to achieve this without becoming a domain administrator or a local administrator on the remote machine. Finally, I was able to find a way to enumerate remotely after providing my scanning user permissions to enumerate through WMI on remote machine. 

- [ ] Open WMImgmt.msc 
- [ ] Go to the Properties of WMI Control
- [ ] Go to the Security Tab
- [ ] Select "Root" and open security
- [ ] Select "Remote Enable" permission for my monitoring user account

![image](https://user-images.githubusercontent.com/46210620/167267924-38147374-04db-4e41-82b8-248ae3f1fd8c.png)

Blogpost Link - [https://rootdse.org/posts/monitoring-realtime-activedirectory-domain-scenarios](https://rootdse.org/posts/monitoring-realtime-activedirectory-domain-scenarios)
