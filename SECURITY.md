# Security Policy

## Supported Versions

We release patches for security vulnerabilities. Which versions are eligible for receiving such patches depends on the CVSS v3.0 Rating:

| Version | Supported          |
| ------- | ------------------ |
| 1.x.x   | :white_check_mark: |

## Reporting a Vulnerability

The BlazorWhiteBoard team takes security bugs seriously. We appreciate your efforts to responsibly disclose your findings, and will make every effort to acknowledge your contributions.

### Where to Report

**Please do not report security vulnerabilities through public GitHub issues.**

Instead, please report security vulnerabilities by emailing the maintainers or opening a private security advisory on GitHub.

To report a vulnerability:

1. Go to the [Security tab](../../security/advisories) of this repository
2. Click "Report a vulnerability"
3. Fill in the details of the vulnerability

Alternatively, you can email details to [your-email@example.com] (replace with actual contact).

### What to Include

Please include the following information in your report:

- Type of issue (e.g., buffer overflow, SQL injection, cross-site scripting, etc.)
- Full paths of source file(s) related to the manifestation of the issue
- The location of the affected source code (tag/branch/commit or direct URL)
- Any special configuration required to reproduce the issue
- Step-by-step instructions to reproduce the issue
- Proof-of-concept or exploit code (if possible)
- Impact of the issue, including how an attacker might exploit it

### Response Timeline

- **Acknowledgment**: We will acknowledge receipt of your vulnerability report within 48 hours
- **Initial Assessment**: We will provide an initial assessment within 5 business days
- **Regular Updates**: We will keep you informed about our progress at least every 7 days
- **Resolution**: We aim to resolve critical issues within 30 days

### What to Expect

After submitting a vulnerability report:

1. The security team will confirm the problem and determine affected versions
2. We will audit code to find any similar problems
3. We will prepare fixes for all supported releases
4. We will release new security fix versions as soon as possible

### Disclosure Policy

- We request that you give us reasonable time to address the issue before public disclosure
- We will credit you in the security advisory (unless you prefer to remain anonymous)
- We will notify you when the vulnerability is fixed
- We will publish a security advisory on GitHub with details of the vulnerability and fix

## Security Best Practices for Users

### Running BlazorWhiteBoard Securely

1. **Use HTTPS in Production**
   - Always use HTTPS in production environments
   - Configure SSL/TLS certificates properly
   - Enable HSTS (HTTP Strict Transport Security)

2. **WebSocket Security**
   - Ensure WebSocket connections use WSS (secure WebSocket) in production
   - Configure SignalR with appropriate authentication if needed

3. **Keep Dependencies Updated**
   ```bash
   dotnet list package --outdated
   dotnet add package [PackageName]
   ```

4. **Environment Configuration**
   - Never commit secrets or API keys to version control
   - Use environment variables or Azure Key Vault for sensitive configuration
   - Use different connection strings for development and production

5. **Input Validation**
   - The application validates user inputs, but be aware of potential XSS risks
   - Usernames are trimmed and limited to 20 characters
   - Drawing data is validated on the server side

6. **Rate Limiting**
   - Consider implementing rate limiting for SignalR connections
   - Monitor for unusual activity or abuse

### Deployment Security

**Azure App Service:**
```bash
# Enable HTTPS only
az webapp update --name <app-name> --resource-group <group> --https-only true

# Enable managed identity
az webapp identity assign --name <app-name> --resource-group <group>
```

**Docker:**
```dockerfile
# Run as non-root user
RUN adduser -u 5678 --disabled-password --gecos "" appuser
USER appuser
```

## Known Security Considerations

### SignalR Connections
- By default, the application allows anonymous connections
- For production use, consider implementing authentication
- Connection IDs are used for user tracking (not sensitive but logged)

### Canvas Content
- Drawing data is broadcast to all connected clients
- There is no persistent storage by default
- Consider the privacy implications of what users might draw

### User Information
- Display names are not authenticated
- Users can choose any name (subject to validation)
- No personal information is collected by default

## Security Updates

Security updates will be announced:

- In the [GitHub Security Advisories](../../security/advisories) section
- In the release notes
- Via GitHub notifications (if you're watching the repository)

## Additional Resources

- [OWASP Top 10](https://owasp.org/www-project-top-ten/)
- [ASP.NET Core Security Best Practices](https://docs.microsoft.com/en-us/aspnet/core/security/)
- [Blazor Security Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/security/)
- [SignalR Security Considerations](https://docs.microsoft.com/en-us/aspnet/core/signalr/security)

## Questions?

If you have questions about this security policy, please open a discussion in the [Discussions](../../discussions) section.

---

Thank you for helping keep BlazorWhiteBoard and its users safe! 🛡️
